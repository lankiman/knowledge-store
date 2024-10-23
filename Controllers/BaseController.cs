using e_learning.Services.Interfaces;
using e_learning.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace e_learning.Controllers
{
    public class BaseController(IUserDetailsService userDetailsService) : Controller
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (User.Identity.IsAuthenticated)
            {

                var userDetails = await userDetailsService!.GetUserDetails();
                if (userDetails != null)
                {
                    context.HttpContext.Items["UserDetails"] = new LayoutsViewModel(userDetails);
                }

                await next();
            }

        }
    }
}
