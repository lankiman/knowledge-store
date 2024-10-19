using e_learning.DataTransfersObjects;
using e_learning.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.Services.Interfaces
{
    public interface IInstructorService
    {
        public Task<UserDto> GetUserDetails();
        public Task<InstructorDto> GetInstructor();

        public Task<IActionResult> CreateLesson(CreateLessonViewModel model);

        public Task<IActionResult> UploadLesson(FileInfo file);

        public Task<List<LessonDto>> GetInstructorLessons();
    }
}