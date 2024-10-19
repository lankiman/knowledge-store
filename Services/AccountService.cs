using e_learning.DataTransfersObjects;
using e_learning.Models;
using e_learning.Services.Interfaces;
using e_learning.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace e_learning.Services;

public class AccountService(
    UserManager<UserModel> userManager,
    SignInManager<UserModel> signInManager,
    IUserDetailsService userDetailsService)
    : BaseService(signInManager, userManager, userDetailsService), IAccountService
{
    private async Task<UserDto?> GetLoggedInUserDetails()
    {
        try
        {
            var user = await UserDetailsService.GetUser();

            if (user != null)
            {
                return new UserDto(user);
            }

            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception in GetLoggedInUserDetails: {ex.Message}");
            return null;
        }
    }

    private async Task<IList<string>?> GetUserRoles()
    {
        try
        {
            var roles = await UserDetailsService.GetUserRole();

            if (roles.Count > 0)
            {
                return roles;
            }

            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception in GetLoggedInUserDetails: {ex.Message}");
            return null;
        }
    }

    private static bool IsValidEmail(string email)
    {
        string pattern = @"^[^\s@]+@[^\s@]+\.[^\s@]+$";
        Regex regex = new(pattern, RegexOptions.IgnoreCase);
        return regex.IsMatch(email);
    }

    //private async Task<string> UseEmailLogin(string loginIdentifier)
    //{

    //    try
    //    {
    //        var result = await userManager!.FindByEmailAsync(loginIdentifier);

    //        if (result != null)
    //        {
    //            return result.UserName!;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine($"Exception in LoginUser: {ex.Message}");
    //        return $"An error occurred: {ex.Message}";
    //    }

    //    return "";
    //}

    private async Task<(string? userName, bool isError, bool notFound)> UseEmailLogin(string loginIdentifier)
    {
        try
        {
            var result = await userManager!.FindByEmailAsync(loginIdentifier);
            return (result?.UserName, false, result == null);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception in LoginUser: {ex.Message}");
            return ($"An error occurred: {ex.Message}", true, false);
        }
    }


    private async Task<UserModel?> GetUser(string userSearchParam)
    {
        var user = await userManager!.FindByNameAsync(userSearchParam);

        if (user != null)
        {
            return user;
        }

        return null;
    }

    [HttpPost]
    private async Task<ActionResult> AssignNewUserRole(UserModel user, string roleName)
    {
        var result = await userManager!.AddToRoleAsync(user, roleName);

        if (result.Succeeded)
        {
            return new OkResult();
        }

        return new BadRequestResult();
    }

    [HttpPost]
    public async Task<LoginResultDto> LoginUser(string loginIdentifier, string password,
        bool rememberMe)
    {

        var (userName, isError, notFound) = IsValidEmail(loginIdentifier)
        ? await UseEmailLogin(loginIdentifier)
        : (loginIdentifier, false, false);


        if (isError)
        {
            return new LoginResultDto(
               new ObjectResult(new { Message = $"An error occurred: {userName}" })
               { StatusCode = 500 }, null, null);
        }

        if (notFound)
        {
            return new LoginResultDto(new UnauthorizedResult(), null, null);
        }

        try
        {
            var result = await signInManager!.PasswordSignInAsync(userName, password, rememberMe, false);

            if (result.Succeeded)
            {
                var user = await GetLoggedInUserDetails();

                if (user != null)
                {
                    var roles = await GetUserRoles();
                    if (roles != null)
                    {
                        return new LoginResultDto(new OkResult(), user,
                            roles);
                    }
                }
            }

            return new LoginResultDto(new UnauthorizedResult(), null, null);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception in LoginUser: {ex.Message}");
            return new LoginResultDto(
                new ObjectResult(new { Message = $"An error occurred: {ex.Message}" })
                { StatusCode = 500 }, null, null);
        }
    }

    public async Task<IActionResult> LogoutUser()
    {
        await signInManager!.SignOutAsync();
        return null!;
    }

    [HttpPost]
    public async Task<RegisterResultDto> RegisterUser(RegisterViewModel newUserEnteredDetails)
    {
        UserModel newUser = new UserModel();
        newUser.UserName = newUserEnteredDetails.Username;
        newUser.Firstname = newUserEnteredDetails.FirstName!;
        newUser.Lastname = newUserEnteredDetails.LastName!;
        newUser.Email = newUserEnteredDetails.Email!;
        newUser.MiddleName = newUserEnteredDetails.MiddleName;
        newUser.PhoneNumber = newUserEnteredDetails.PhoneNumber;

        try
        {
            var result = await userManager!.CreateAsync(newUser, newUserEnteredDetails.Password!);

            if (result.Succeeded)
            {
                var user = GetUser(newUserEnteredDetails.Username!);

                await AssignNewUserRole(user.Result!, "Learner");

                return new RegisterResultDto(new OkResult(), result);
            }

            return new RegisterResultDto(new BadRequestResult(), result);
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine($"Exception in RegisterUser: {ex.InnerException.Message}");

            if (ex.InnerException.Message.Contains("IX_AspNetUsers_PhoneNumber"))
            {
                return new RegisterResultDto(
                    new ConflictObjectResult(new string("The Phone number is already in use")) { StatusCode = 409 },
                    null!);
            }

            return new RegisterResultDto(new ObjectResult(new { Message = "An Error Occured" }) { StatusCode = 409 },
                null!);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception in RegisterUser: {ex.InnerException.Message}");
            return new RegisterResultDto(
                new ObjectResult(new { Message = "Server Error Occured" })
                { StatusCode = 500 }, null!);
        }
    }
}