using e_learning.Data;
using e_learning.DataTransfersObjects;
using e_learning.Models;
using e_learning.Services.Interfaces;
using e_learning.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;

namespace e_learning.Services
{
    public class InstructorService(
        ELearningDbContext eLearningContext,
        IUserDetailsService userDetailsService,
        IWebHostEnvironment webHostEnvironment)
        : BaseService(eLearningContext, webHostEnvironment, userDetailsService), IInstructorService
    {
        private readonly string _contentRoot = webHostEnvironment.ContentRootPath;


        public async Task<InstructorDto> GetAuthenticatedInstructor()
        {
            var user = await UserDetailsService!.GetUser();

            var userLessons = await GetAuthenticatedInstructorLessons();

            if (user != null)
            {
                user.UserOwnedLessons = userLessons;
            }

            return new InstructorDto(user!);
        }

        private string CreateVideoFileStorageDirectory()
        {
            var eLearningVideosFolder = Path.Combine(_contentRoot, "ELearning_Videos");
            Directory.CreateDirectory(eLearningVideosFolder);

            var e = new DirectoryInfo(eLearningVideosFolder);

            return eLearningVideosFolder;
        }

        private async Task<IActionResult> SaveLessonDetailsToDB(CreateLessonViewModel model, string modelVideoUrl)
        {
            try
            {
                var lessonOwnerId = await GetAuthenticatedInstructor();
                var newLesson = new LessonModel();

                newLesson.LessonName = model.LessonName;
                newLesson.LessonCategory = model.LessonCategory;
                newLesson.LessonVideoUrl = modelVideoUrl;
                newLesson.LessonOwnerId = lessonOwnerId.Id;
                newLesson.LessonDescription = model.LessonDescription;

                await eLearningContext.Lessons.AddAsync(newLesson);


                var result = await eLearningContext.SaveChangesAsync();

                if (result > 0)
                {
                    return new OkResult();
                }

                return new BadRequestResult();
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { Message = "Server Error Occured" })
                    { StatusCode = 500 };
            }
        }

        private async Task<IActionResult> SaveLessonVideoToStorage(CreateLessonViewModel model, string videoFilePath)
        {
            try
            {
                using (var fileStream = new FileStream(videoFilePath, FileMode.Create))
                {
                    await model.LessonVideo.CopyToAsync(fileStream);
                }

                return new OkResult();
            }
            catch (Exception ex)
            {
                File.Delete(videoFilePath);
                return new ObjectResult(new { Message = "Server Error Occured" })
                    { StatusCode = 500 };
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateLesson(CreateLessonViewModel model)
        {
            var eLearningVideosFolder = CreateVideoFileStorageDirectory();
            var videoFilePath = Path.Combine(eLearningVideosFolder, Path.GetRandomFileName());

            var videoSavingResult = await SaveLessonVideoToStorage(model, videoFilePath);

            switch (videoSavingResult)
            {
                case OkResult:
                    var videoDetailsSavingResult = await SaveLessonDetailsToDB(model, videoFilePath);
                    if (videoDetailsSavingResult is OkResult)
                    {
                        return new OkObjectResult("Video Saved Successfully");
                    }

                    File.Delete(videoFilePath);
                    return new ObjectResult(new { Message = "Server Error Occured" })
                        { StatusCode = 500 };
                    break;
            }

            return new ObjectResult(new { Message = "Server Error Occured" })
                { StatusCode = 500 };
        }

        public async Task<List<LessonModel>> GetAuthenticatedInstructorLessons()
        {
            var authenticatedInstructorId = await UserDetailsService!.GetUser();

            var lessons =
                await eLearningContext.Lessons.Where(lesson => lesson.LessonOwnerId == authenticatedInstructorId.Id)
                    .ToListAsync();

            return lessons;
        }
    }
}