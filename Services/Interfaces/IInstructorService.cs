using e_learning.DataTransfersObjects;
using e_learning.ViewModels;
using e_learning.Views.Instructor.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.Services.Interfaces
{
    public interface IInstructorService
    {
        public Task<UserDto> GetUserDetails();
        public Task<InstructorDto> GetInstructor();

        public Task<IActionResult> CreateLesson(ViewModels.CreateLessonViewModel model);

        public Task<IActionResult> CompleteLessonDetails(Views.Instructor.ViewModels.CreateLessonViewModel lessonData, string tempLessonId);

        public Task<IActionResult> UploadLessonToTempStorage(IFormFile file);

        public Task<List<LessonDto>> GetInstructorLessons();
    }
}