using e_learning.Data;
using e_learning.DataTransfersObjects;
using e_learning.Enums;
using e_learning.Models;
using e_learning.Services.Interfaces;
using e_learning.Views.Instructor.ViewModels;
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

            var mainFolder = Path.Combine(_contentRoot, "ELearning_Storage");
            Directory.CreateDirectory(mainFolder);
            Directory.CreateDirectory(Path.Combine(mainFolder, "Videos"));
            Directory.CreateDirectory(Path.Combine(mainFolder, "Videos", "Temp_Videos"));
            Directory.CreateDirectory(Path.Combine(mainFolder, "Videos", "Published_Videos"));
            Directory.CreateDirectory(Path.Combine(mainFolder, "Thumbnails"));
            Directory.CreateDirectory(Path.Combine(mainFolder, "Thumbnails", "Temp_Thumbnails"));
            Directory.CreateDirectory(Path.Combine(mainFolder, "Thumbnails", "Published_Thumbnails"));
            Directory.CreateDirectory(Path.Combine(mainFolder, "Profile_Pics"));

            return mainFolder;
        }

        private string GetVideoTempStorage()

        {
            var mainFolder = GetVideoFileStorageDirectory();
            var tempVideosFolder = Path.Combine(mainFolder, "Videos", "Temp_Videos");
            return tempVideosFolder;
        }
        private string GetVideoTempThumbnialStorage()
        {
            var mainFolder = GetVideoFileStorageDirectory();
            var tempThumbnailsFolder = Path.Combine(mainFolder, "Thumbnails", "Temp_Thumbnails");
            return tempThumbnailsFolder;
        }

        private static void TryDeleteFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            catch (IOException)
            {

            }
            catch (UnauthorizedAccessException)
            {

            }
        }

        private static async Task<IActionResult> SaveLessonVideoToTempStorage(IFormFile file, string videoFilePath)
        {

            if (file == null)
            {
                TryDeleteFile(videoFilePath);
                return new ObjectResult(new { Message = "File is null or Upload Canceled" }) { StatusCode = 499 };
            }
            try
            {
                using (var fileStream = new FileStream(videoFilePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                return new OkResult();
            }
            catch (OperationCanceledException)
            {
                TryDeleteFile(videoFilePath);
                return new ObjectResult(new { Message = "Upload canceled." }) { StatusCode = 499 };
            }
            catch (Exception ex)
            {
                TryDeleteFile(videoFilePath);
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

                var newLesson = new TemporaryLessonModel
                {
                    LessonVideoStatus = LessonVideoStatus.Draft,
                    LessonOwnerId = ownerId,
                    TempLessonUrl = videoUrl
                };

                await eLearningContext.TemporaryLessons.AddAsync(newLesson);
                var result = await eLearningContext.SaveChangesAsync();

                if (result > 0)
                {
                    return new OkObjectResult(newLesson.Id);
                }

                return new BadRequestResult();
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { Message = "Server Error Occured" })
                { StatusCode = 500 };
            }
        }

        private async Task<IActionResult> CompleteLessonVideoDetailsToTempDB(CreateLessonViewModel lessonData, string tempLessonId, string thumbnailUrl)
        {
            try
            {
                var newTempLessonDetials = new TemporaryLessonDetailsModel
                {
                    TemporaryLessonId = tempLessonId,
                    TemporaryLessonCategory = (LessonCategory)lessonData.LessonCategory,
                    TemporaryLessonName = lessonData.LessonName,
                    TemporaryLessonDescription = lessonData.LessonDescription,
                    TemporaryLessonAcessType = (AcessType)lessonData.LessonAcessType,
                    TemporaryLessonThumbnailUrl = thumbnailUrl
                };

                await eLearningContext.TemporaryLessonsDetails.AddAsync(newTempLessonDetials);
                var result = await eLearningContext.SaveChangesAsync();
                if (result > 0)
                {
                    return new OkResult();
                }

                return new BadRequestResult();


            }
            catch (Exception ex)
            {
                Console.WriteLine("HERE IS THE EXCEPTION", ex);
                return new ObjectResult(new { Message = "Server Error Occured" })
                { StatusCode = 500 };
            }

        }


        private static async Task<IActionResult> SaveLessonThumbnailToTempStorage(IFormFile file, string thumbnailFilePath)
        {
            try
            {
                using (var fileStream = new FileStream(thumbnailFilePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                return new OkResult();
            }
            catch (Exception ex)
            {
                TryDeleteFile(thumbnailFilePath);
                return new ObjectResult(new { Message = "Server Error Occured" })
                { StatusCode = 500 };
            }
        }


        public async Task<IActionResult> UploadLessonToTempStorage(IFormFile file)
        {
            var tempStorageFolder = GetVideoTempStorage();
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
                            return new OkObjectResult(new { Message = "Video Upload Sucessful", lessonId });
                        }
                        TryDeleteFile(videoFilePath);
                        return new ObjectResult(new { Message = "Server Error Occured" })
                        { StatusCode = 500 };
                }


            }
            catch (Exception ex)
            {
                TryDeleteFile(videoFilePath);
                return new ObjectResult(new { Message = "Server Error Occured" })
                { StatusCode = 500 };
            }
            TryDeleteFile(videoFilePath);
            return new ObjectResult(new { Message = "Server Error Occured" })
            { StatusCode = 500 };

        }

        public async Task<IActionResult> CompleteLessonDetails(CreateLessonViewModel lessonData, string templessonId)
        {
            var tempThumbnailPath = GetVideoTempThumbnialStorage();
            var tempName = Path.GetRandomFileName();
            var thumbnailExtension = Path.GetExtension(lessonData.LessonThumbnail.FileName);
            var thumbnailName = $"{Path.GetFileNameWithoutExtension(tempName)}{thumbnailExtension}";
            var thumbnailFilePath = Path.Combine(tempThumbnailPath, thumbnailName);
            try
            {
                var thumbnailSavingResult = await SaveLessonThumbnailToTempStorage(lessonData.LessonThumbnail, thumbnailFilePath);
                switch (thumbnailSavingResult)
                {
                    case OkResult:
                        var completeLessonDetialsResult = await CompleteLessonVideoDetailsToTempDB(lessonData, templessonId, thumbnailFilePath);
                        if (completeLessonDetialsResult is OkResult)
                        {
                            return new OkObjectResult(new { Message = "Details Completed Sucessful" });
                        }
                        TryDeleteFile(thumbnailFilePath);
                        return new ObjectResult(new { Message = "Server Error Occured While Saving Lesson Details" })
                        { StatusCode = 500 };
                }

            }
            catch (Exception ex)
            {
                TryDeleteFile(thumbnailFilePath);
                return new ObjectResult(new { Message = "Server Error" })
                { StatusCode = 500 };
            }
            TryDeleteFile(thumbnailFilePath);
            return new ObjectResult(new { Message = "An Error Occured" })
            { StatusCode = 500 };

        }

        public async Task<List<LessonDto>> GetInstructorLessons()
        {
            var instructorId = await UserDetailsService!.GetUserId();

            if (instructorId == "")
            {
                return null!;
            }

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