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
        protected readonly IUserDetailsService? UserDetailsService;
        protected readonly IHttpContextAccessor? httpContextAccessor;


        /// <summary>
        /// Constructor for services Requiring all dependencies
        /// </summary>
        /// <param name="eLearningContext"></param>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        protected BaseService(ELearningDbContext eLearningContext, UserManager<UserModel> userManager,
            SignInManager<UserModel> signInManager, IUserDetailsService userDetailsService)
        {
            this.eLearningContext = eLearningContext;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.UserDetailsService = userDetailsService;
        }

        /// <summary>
        /// Constructor For Account Service (or services requiring only signin manager, user manager and current user service)
        /// </summary>
        /// <param name="signInManager"></param>
        /// <param name="userManager"></param>
        /// <param name="userDetailsService"></param>
        protected BaseService(SignInManager<UserModel> signInManager, UserManager<UserModel> userManager,
            IUserDetailsService userDetailsService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.UserDetailsService = userDetailsService;
        }

        /// <summary>
        /// Constructor for Admin service (or for services requiring db context, user manager and current user service)
        /// </summary>
        /// <param name="eLearningContext"></param>
        /// <param name="userManager"></param>
        /// <param name="userDetailsService"></param>
        protected BaseService(ELearningDbContext eLearningContext, UserManager<UserModel> userManager,
            IUserDetailsService userDetailsService)
        {
            this.eLearningContext = eLearningContext;
            this.userManager = userManager;
            this.UserDetailsService = userDetailsService;
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
        /// <param name="userDetailsService"></param>
        protected BaseService(ELearningDbContext eLearningContext, IUserDetailsService userDetailsService)
        {
            this.eLearningContext = eLearningContext;
            this.UserDetailsService = userDetailsService;
        }


        // Protected property to access HttpContext directly in derived classes
        // protected HttpContext HttpContext => httpContextAccessor!.HttpContext!;
    }
}