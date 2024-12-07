using System;
using Azure.Core;
using Biometrics.Enums;
using Biometrics.Models;
using Microsoft.AspNetCore.Identity;

namespace Biometrics.Services
{
	public class UserService : IUserService
	{
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<UserService> _logger;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<UserService> logger)
        {
            _userManager = userManager;
			_signInManager = signInManager;
            _logger = logger;
        }

        public async Task<Response<bool>> RegisterUserAsync(RegisterModel model)
        {
            _logger.LogInformation("WELCOME TO REGISTER USER SERVICE");
            var response = "";
            try
            {
                var userWithSameUserName = await _userManager.FindByNameAsync(model.UserName);
                if (userWithSameUserName != null)
                {
                    response = $"UserName {model.UserName} already exist";
                    _logger.LogInformation(response);
                    return await Task.FromResult(new Response<bool>() { Code = (long)ApiResponseCodes.INVALID_REQUEST, Description = response, Data = false });
                }
                var user = new ApplicationUser
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.UserName
                };
                var userWithSameEmail = await _userManager.FindByEmailAsync(model.Email);
                if (userWithSameEmail == null)
                {
                    user.EmailConfirmed = true;
                    user.Status = (int)UserStatuses.Active;
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        //await _userManager.AddToRoleAsync(user, model.Role);
                        response = $"User {model.UserName} Enrolled successfully";
                        _logger.LogInformation(response);
                        return await Task.FromResult(new Response<bool>() { Code = (long)ApiResponseCodes.OK, Description = response, Data = true });
                    }
                    else
                    {
                        response = $"Failed to Save New User :: ";
                        List<string> errors = new List<string>();
                        foreach (var error in result.Errors)
                        {
                            string errorVal = error.Description;
                            response += errorVal;
                            errors.Add(errorVal);
                        }
                        _logger.LogError(response);
                        return await Task.FromResult(new Response<bool>() { Code = (long)ApiResponseCodes.ERROR, Description = response, Errors = errors });
                    }
                }
                else
                {
                    response = $"Email - {model.Email} used already exist";
                    _logger.LogError(response);
                    return await Task.FromResult(new Response<bool>() { Code = (long)ApiResponseCodes.INVALID_REQUEST, Description = response, Data = false });
                }
            }
            catch (Exception eex)
            {
                _logger.LogError($"USER REGISTRATION EXCEPTION:: {eex?.ToString()}");
                response = eex?.Message;
                return await Task.FromResult(new Response<bool>() { Code = (long)ApiResponseCodes.EXCEPTION, Description = response });
            }
        }
    }
}