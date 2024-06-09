using e_learning.Data;
using e_learning.Services.Interfaces;

namespace e_learning.Services
{
    public class AdminService : BaseService, IAdminService
    {
        public AdminService(ELearningDbContext ELearningContext) : base(ELearningContext)
        {
        }
    }
}