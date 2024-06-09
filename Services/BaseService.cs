using e_learning.Data;

namespace e_learning.Services
{
    public class BaseService
    {
        protected readonly ELearningDbContext _eLearnincContext;

        public BaseService(ELearningDbContext ELearningContext)
        {
            _eLearnincContext = ELearningContext;
        }
    }
}