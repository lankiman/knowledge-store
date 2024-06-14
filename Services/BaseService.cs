using e_learning.Data;
using e_learning.Models;
using Microsoft.AspNetCore.Identity;

namespace e_learning.Services
{
    public class BaseService
    {
        protected readonly ELearningDbContext? eLearningContext;
        protected readonly UserManager<UserModel>? userManager;
        protected readonly SignInManager<UserModel>? signInManager;
        protected readonly IHttpContextAccessor? httpContextAccessor;

        /// <summary>
        /// Constructor for services Requiring all dependencies
        /// </summary>
        /// <param name="eLearningContext"></param>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="httpContextAccessor"></param>
        protected BaseService(ELearningDbContext eLearningContext, UserManager<UserModel> userManager,
            SignInManager<UserModel> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            this.eLearningContext = eLearningContext;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// constructor for services requiring signin, user, httpContext
        /// </summary>
        /// <param name="signInManager"></param>
        /// <param name="userManager"></param>
        /// <param name="httpContextAccessor"></param>
        protected BaseService(SignInManager<UserModel> signInManager, UserManager<UserModel> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Constructor for services requiring eLearningContext, userManger and httpContextAccessor
        /// </summary>
        /// <param name="eLearningContext"></param>
        /// <param name="userManager"></param>
        /// <param name="httpContextAccessor"></param>
        protected BaseService(ELearningDbContext eLearningContext, UserManager<UserModel> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            this.eLearningContext = eLearningContext;
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }


        // Protected property to access HttpContext directly in derived classes
        protected HttpContext HttpContext => httpContextAccessor!.HttpContext!;
    }
}