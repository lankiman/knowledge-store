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


        public async Task<UserDto> GetUserDetails()
        {
            var user = await UserDetailsService.GetUser();
            if (user == null)
            {
                return null;
            }
            return new UserDto(user);
        }

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

        private string GetVideoFileStorageDirectory()
        {
            var eLearningVideosFolder = Path.Combine(_contentRoot, "ELearning_Videos");
            Directory.CreateDirectory(eLearningVideosFolder);

            var e = new DirectoryInfo(eLearningVideosFolder);
            e.CreateSubdirectory("Temp_Videos");

            return eLearningVideosFolder;
        }

        private string GetVideoTempStorage(string path)
        {
            var tempVideosFolder = Path.Combine(path, "Temp_Videos");
            return tempVideosFolder;
        }

        private async Task<IActionResult> SaveLessonVideoToTempStorage(IFormFile file, string videoFilePath)
        {
            try
            {
                using (var fileStream = new FileStream(videoFilePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
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

        private async Task<IActionResult> SaveLessonVideoDetailsToTempDB(string videoUrl)
        {
            try
            {
                var ownerId = await UserDetailsService!.GetUserId();

                if (ownerId == "")
                {
                    return null!;
                }

                var newLesson = new TemporaryLessonModel();
                newLesson.LessonVideoStatus = Enums.LessonVideoStatus.Draft;
                newLesson.LessonOwnerId = ownerId;
                newLesson.TempLessonUrl = videoUrl;



                await eLearningContext.TemporaryLessons.AddAsync(newLesson);
                var result = await eLearningContext.SaveChangesAsync();

                if (result > 0)
                {
                    return new OkObjectResult(new { newLesson.Id });
                }

                return new BadRequestResult();
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { Message = "Server Error Occured" })
                { StatusCode = 500 };
            }
        }


        public async Task<IActionResult> UploadLessonToTemp(IFormFile file)
        {
            var eLearningVideoFolder = GetVideoFileStorageDirectory();
            var tempStorageFolder = GetVideoTempStorage(eLearningVideoFolder);
            var tempName = Path.GetRandomFileName();
            var videoName = $"{Path.GetFileNameWithoutExtension(tempName)}.mp4";
            var videoFilePath = Path.Combine(tempStorageFolder, videoName);

            try
            {
                var videoSavingResult = await SaveLessonVideoToTempStorage(file, videoFilePath);

                switch (videoSavingResult)
                {
                    case OkResult:
                        var videoDetialsSavingResult = await SaveLessonVideoDetailsToTempDB(videoFilePath);
                        if (videoDetialsSavingResult is OkObjectResult result)
                        {
                            var lessonId = result.Value;
                            return new OkObjectResult(new { Message = "Video Upload Sucessful", Id = lessonId });
                        }
                        File.Delete(videoFilePath);
                        return new ObjectResult(new { Message = "Server Error Occured" })
                        { StatusCode = 500 };
                }


            }
            catch (Exception ex)
            {
                return new ObjectResult(new { Message = "Server Error Occured" })
                { StatusCode = 500 };
            }

            return new ObjectResult(new { Message = "Server Error Occured" })
            { StatusCode = 500 };

        }

        //private async Task<IActionResult> SaveLessonDetailsToDB(CreateLessonViewModel model, string modelVideoUrl)
        //{
        //    try
        //    {
        //        var ownerId = await UserDetailsService!.GetUserId();

        //        if (ownerId == "")
        //        {
        //            return null!;
        //        }

        //        var newLesson = new LessonModel();
        //        newLesson.LessonOwnerId = ownerId;
        //        newLesson.LessonName = model.LessonName;
        //        newLesson.LessonCategory = model.LessonCategory;
        //        newLesson.LessonVideoUrl = modelVideoUrl;
        //        newLesson.LessonDescription = model.LessonDescription;

        //        await eLearningContext.Lessons.AddAsync(newLesson);
        //        var result = await eLearningContext.SaveChangesAsync();

        //        if (result > 0)
        //        {
        //            return new OkResult();
        //        }

        //        return new BadRequestResult();
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ObjectResult(new { Message = "Server Error Occured" })
        //        { StatusCode = 500 };
        //    }
        //}




        //public async Task<IActionResult> CreateLesson(CreateLessonViewModel model)
        //{
        //    var eLearningVideosFolder = CreateVideoFileStorageDirectory();

        //    var tempName = Path.GetRandomFileName();

        //    var videoName = $"{Path.GetFileNameWithoutExtension(tempName)}.mp4";


        //    var videoFilePath = Path.Combine(eLearningVideosFolder, videoName);

        //    var videoSavingResult = await SaveLessonVideoToStorage(model, videoFilePath);

        //    switch (videoSavingResult)
        //    {
        //        case OkResult:
        //            var videoDetailsSavingResult = await SaveLessonDetailsToDB(model, videoFilePath);
        //            if (videoDetailsSavingResult is OkResult)
        //            {
        //                return new OkObjectResult("Video Saved Successfully");
        //            }

        //            File.Delete(videoFilePath);
        //            return new ObjectResult(new { Message = "Server Error Occured" })
        //            { StatusCode = 500 };
        //    }

        //    return new ObjectResult(new { Message = "Server Error Occured" })
        //    { StatusCode = 500 };
        //}

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
                if (!File.Exists(lesson.LessonVideoUrl))
                {
                    return null;
                }

                var lessonDto = new LessonDto(lesson);

                lessons.Add(lessonDto);
            }

            return lessons;
        }



        public Task<IActionResult> CreateLesson(CreateLessonViewModel model)
        {
            throw new NotImplementedException();
        }


    }
}