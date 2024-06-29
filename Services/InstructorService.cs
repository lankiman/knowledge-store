using e_learning.Data;
using e_learning.DataTransfersObjects;
using e_learning.Models;
using e_learning.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace e_learning.Services
{
    public class InstructorService(
        ELearningDbContext eLearningContext,
        IUserDetailsService userDetailsService)
        : BaseService(eLearningContext, userDetailsService), IInstructorService
    {
        public async Task<CreatorDto> GetAuthenticatedInstructor()
        {
            var user = await UserDetailsService!.GetUser();

            return new CreatorDto(user!);
        }
    }
}