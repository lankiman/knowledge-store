using e_learning.Data;
using e_learning.Services.Interfaces;

namespace e_learning.Services
{
    public class LessonService : BaseService, ILessonService
    {
        public LessonService(ELearningDbContext ELearningContext) : base(ELearningContext)
        {
        }
    }
}