using e_learning.Data;
using e_learning.Services.Interfaces;

namespace e_learning.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(ELearningDbContext ELearningContext) : base(ELearningContext)
        {
        }
    }
}