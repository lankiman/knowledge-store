﻿using e_learning.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;

namespace e_learning.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<IActionResult> LoginUser(string loginIdentifier, string password, bool rememberMe);

        public Task<IActionResult> LogoutUser();

        public Task<IActionResult> RegisterUser();
    }
}