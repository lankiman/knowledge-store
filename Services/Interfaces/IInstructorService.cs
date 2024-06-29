using e_learning.DataTransfersObjects;

namespace e_learning.Services.Interfaces
{
    public interface IInstructorService
    {
        public Task<CreatorDto> GetAuthenticatedInstructor();
    }
}