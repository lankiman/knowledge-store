using e_learning.Models;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.Services.Interfaces
{
    public interface ILessonService
    {
        Task<LessonModel> GetAllLessonAsync();

        Task<IActionResult> PlayVideo(string filePath);
    }
}