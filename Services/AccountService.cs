﻿using e_learning.DataTransfersObjects;
using e_learning.Models;
using e_learning.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace e_learning.Services;

public class AccountService(SignInManager<UserModel> signInManager, UserManager<UserModel> userManager)
    : IAccountService
{
    private async Task<UserModel?> GetLoggedInUserDetails(string loginIdentifier)
    {
        try
        {
            var user = await userManager.FindByEmailAsync(loginIdentifier) ??
                       await userManager.FindByNameAsync(loginIdentifier);

            if (user != null)
            {
                return user;
            }

            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception in GetLoggedInUserDetails: {ex.Message}");
            return null;
        }
    }

    private async Task<IList<string>?> GetUserRoles(UserModel user)
    {
        try
        {
            var roles = await userManager.GetRolesAsync(user);

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


    public async Task<LoginResultDTO> LoginUser(string loginIdentifier, string password,
        bool rememberMe)
    {
        try
        {
            var result = await signInManager.PasswordSignInAsync(loginIdentifier, password, rememberMe, false);

            if (result.Succeeded)
            {
                var user = await GetLoggedInUserDetails(loginIdentifier);

                if (user != null)
                {
                    var roles = await GetUserRoles(user);
                    if (roles != null)
                    {
                        return new LoginResultDTO(new OkResult(), user,
                            roles);
                    }
                }
            }

            return new LoginResultDTO(new UnauthorizedResult(), null, null);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception in LoginUser: {ex.Message}");
            return new LoginResultDTO(
                new ObjectResult(new { Message = $"An error occurred: {ex.Message}" })
                    { StatusCode = 500 }, null, null);
        }
    }


    public async Task<IActionResult> LogoutUser()
    {
        throw new NotImplementedException();
    }

    public async Task<IActionResult> RegisterUser()
    {
        throw new NotImplementedException();
    }
}