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
        protected readonly ICurrentUserService? currentUserService;
        protected readonly IHttpContextAccessor? httpContextAccessor;


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
        /// Constructor For Account Service (or services requiring only signin manager, user manager and current user service)
        /// </summary>
        /// <param name="signInManager"></param>
        /// <param name="userManager"></param>
        /// <param name="currentUserService"></param>
        protected BaseService(SignInManager<UserModel> signInManager, UserManager<UserModel> userManager,
            ICurrentUserService currentUserService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.currentUserService = currentUserService;
        }

        /// <summary>
        /// Constructor for Admin service (or for services requiring db context, user manager and current user service)
        /// </summary>
        /// <param name="eLearningContext"></param>
        /// <param name="userManager"></param>
        /// <param name="currentUserService"></param>
        protected BaseService(ELearningDbContext eLearningContext, UserManager<UserModel> userManager,
            ICurrentUserService currentUserService)
        {
            this.eLearningContext = eLearningContext;
            this.userManager = userManager;
            this.currentUserService = currentUserService;
        }

        /// <summary>
        /// Constructor for Current user Service (or for services requiring only user manager and httpcontext)
        /// </summary>
        /// <param name="userManager"></param>
        protected BaseService(UserManager<UserModel> userManager, IHttpContextAccessor httpContextAccessor)
        {
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Constructor for Creator Service (or services requiring only db context and current user service
        /// </summary>
        /// <param name="eLearningDbContext"></param>
        /// <param name="currentUserService"></param>
        protected BaseService(ELearningDbContext eLearningContext, ICurrentUserService currentUserService)
        {
            this.eLearningContext = eLearningContext;
            this.currentUserService = currentUserService;
        }


        // Protected property to access HttpContext directly in derived classes
        // protected HttpContext HttpContext => httpContextAccessor!.HttpContext!;
    }
}