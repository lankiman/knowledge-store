using e_learning.Data;
using e_learning.DataTransfersObjects;
using e_learning.Models;
using e_learning.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace e_learning.Services
{
    public class CreatorService(
        ELearningDbContext eLearningContext,
        IUserDetailsService userDetailsService)
        : BaseService(eLearningContext, userDetailsService), ICreatorService
    {
        public async Task<CreatorDto> GetAuthenticatedCreator()
        {
            var user = await UserDetailsService!.GetUser();

            return new CreatorDto(user!);
        }
    }
}