using e_learning.Data;
using e_learning.Models;
using e_learning.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace e_learning.Services
{
    public class BaseService
    {
        protected readonly ELearningDbContext? eLearningContext;
        protected readonly UserManager<UserModel>? userManager;
        protected readonly SignInManager<UserModel>? signInManager;
        protected readonly ICurrentUserService currentUserService;
        protected readonly IHttpContextAccessor httpContextAccessor;


        /// <summary>
        /// Constructor for services Requiring all dependencies
        /// </summary>
        /// <param name="eLearningContext"></param>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        protected BaseService(ELearningDbContext eLearningContext, UserManager<UserModel> userManager,
            SignInManager<UserModel> signInManager, ICurrentUserService currentUserService)
        {
            this.eLearningContext = eLearningContext;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.currentUserService = currentUserService;
        }

        /// <summary>
        /// constructor for services requiring signin, user, current user service
        /// </summary>
        /// <param name="signInManager"></param>
        /// <param name="userManager"></param>
        protected BaseService(SignInManager<UserModel> signInManager, UserManager<UserModel> userManager,
            ICurrentUserService currentUserService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.currentUserService = currentUserService;
        }

        /// <summary>
        /// Constructor for services requiring eLearningContext, userManger and currentuserservice
        /// </summary>
        /// <param name="eLearningContext"></param>
        /// <param name="userManager"></param>
        protected BaseService(ELearningDbContext eLearningContext, UserManager<UserModel> userManager,
            ICurrentUserService currentUserService)
        {
            this.eLearningContext = eLearningContext;
            this.userManager = userManager;
            this.currentUserService = currentUserService;
        }

        /// <summary>
        /// Constructor for services requiring only usermanger
        /// </summary>
        /// <param name="userManager"></param>
        protected BaseService(UserManager<UserModel> userManager)
        {
            this.userManager = userManager;
        }


        // Protected property to access HttpContext directly in derived classes
        protected HttpContext HttpContext => httpContextAccessor!.HttpContext!;
    }
}