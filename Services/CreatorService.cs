using e_learning.Data;
using e_learning.DataTransfersObjects;
using e_learning.Models;
using e_learning.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace e_learning.Services
{
    public class CreatorService(
        ELearningDbContext eLearningContext,
        ICurrentUserService currentUserService)
        : BaseService(eLearningContext, currentUserService), ICreatorService
    {
        public async Task<CreatorDto> GetAuthenticatedCreator()
        {
            var user = await currentUserService!.GetCurrentUser();

            return new CreatorDto(user!);
        }
    }
}