using e_learning.Services;
using e_learning.Services.Default;
using e_learning.Services.Interfaces;

namespace e_learning.Extensions
{
    public static class ELearningBusinessServicesCollectionExtensions
    {
        public static void AddELearningBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<ILessonService, LessonService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccountService, AccountService>();


            services.AddScoped<RoleInitializerService>();
            services.AddScoped<AdminUserInitializerService>();
        }
    }
}