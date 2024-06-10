using e_learning.Models;

namespace e_learning.Services.Interfaces
{
    public interface ILessonService
    {
        Task<LessonModel> GetAllLessonAsync();
    }
}