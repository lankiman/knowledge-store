using e_learning.Data;
using e_learning.DataTransfersObjects;
using e_learning.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.Services
{
    public class InstructorService(
        ELearningDbContext eLearningContext,
        IUserDetailsService userDetailsService,
        IWebHostEnvironment webHostEnvironment)
        : BaseService(eLearningContext, webHostEnvironment, userDetailsService), IInstructorService
    {
        private readonly string _contentRoot = webHostEnvironment.ContentRootPath;


        private string CreateVideoFileStorageDirectory()
        {
            var eLearningVideosFolder = Path.Combine(_contentRoot, "ELearning_Videos");
            Directory.CreateDirectory(eLearningVideosFolder);
            return eLearningVideosFolder;
        }

        // private async Task<IActionResult> SaveLessonDetailsToDB()
        // {
        // }

        public async Task<InstructorDto> GetAuthenticatedInstructor()
        {
            var user = await UserDetailsService!.GetUser();

            return new InstructorDto(user!);
        }

        public Task<IActionResult> CreateLesson()
        {
            throw new NotImplementedException();
        }
    }
}