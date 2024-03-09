using Microsoft.AspNetCore.Identity;
using MVC.Application.Services.Interfaces.User;
using MVC.Domain.Helper;
using MVC.Domain.Models;
using MVC.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Application.Services.Implementations.User;
public class UserService : IUserService
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public UserService(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }
    public async Task<ResponseResult> Login(LoginViewModel model)
    {
        var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, true, false);
      if (result.Succeeded)
      {
          return new ResponseResult { IsSuccess = true };
      }
        return new ResponseResult { IsSuccess =false};
    }

    public async Task<ResponseResult> Register(RegisterViewModel model)
    {
        var user = new IdentityUser { UserName = model.UserName, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            return new ResponseResult { IsSuccess = true };
        }
       return new ResponseResult {IsSuccess =false };
    } 
    public async Task<ResponseResult> SignOutAsync()
    {
       await _signInManager.SignOutAsync();
       return new ResponseResult {IsSuccess =true };
    }
}

