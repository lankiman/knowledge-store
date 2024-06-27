using e_learning.DataTransfersObjects;

namespace e_learning.Services.Interfaces
{
    public interface ICreatorService
    {
        public Task<CreatorDto> GetAuthenticatedCreator();
    }
}