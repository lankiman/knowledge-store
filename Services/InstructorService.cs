using e_learning.Data;
using e_learning.DataTransfersObjects;
using e_learning.Models;
using e_learning.Services.Interfaces;
using e_learning.ViewModels;
using Microsoft.AspNetCore.Mvc;
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


        public async Task<InstructorDto> GetInstructor()
        {
            var userId = await UserDetailsService!.GetUserId();

            if (userId == "")
            {
                return null!;
            }

            var instructor = await eLearningContext.Instructors.Include(i => i.User).Include(i => i.InstructorLessons)
                .FirstOrDefaultAsync(i => i.Id == userId);

            if (instructor == null)
            {
                return null!;
            }

            return new InstructorDto(instructor);
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
                var ownerId = await UserDetailsService!.GetUserId();

                if (ownerId == "")
                {
                    return null!;
                }

                var newLesson = new LessonModel();
                newLesson.LessonOwnerId = ownerId;
                newLesson.LessonName = model.LessonName;
                newLesson.LessonCategory = model.LessonCategory;
                newLesson.LessonVideoUrl = modelVideoUrl;
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


        public async Task<IActionResult> CreateLesson(CreateLessonViewModel model)
        {
            var eLearningVideosFolder = CreateVideoFileStorageDirectory();

            var tempName = Path.GetRandomFileName();

            var videoName = $"{Path.GetFileNameWithoutExtension(tempName)}.mp4";


            var videoFilePath = Path.Combine(eLearningVideosFolder, videoName);

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
            }

            return new ObjectResult(new { Message = "Server Error Occured" })
                { StatusCode = 500 };
        }

        public async Task<List<LessonDto>> GetInstructorLessons()
        {
            var instructorId = await UserDetailsService!.GetUserId();

            if (instructorId == "")
            {
                return null!;
            }

            // var lessons =
            //     await eLearningContext.Lessons.Where(lesson => lesson.LessonOwnerId == instructorId)
            //         .ToListAsync();
            // return lessons;


            var inr = await GetInstructor();

            if (inr == null)
            {
                return null;
            }

            var lessons = new List<LessonDto>();

            foreach (var lesson in inr.InstructorLessons)
            {
                var lessonDto = new LessonDto(lesson);

                lessons.Add(lessonDto);
            }

            return lessons;
        }
    }
}