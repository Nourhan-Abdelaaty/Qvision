using Microsoft.AspNetCore.Mvc;
using MVC.Application.Services.Implementations.User;
using MVC.Application.Services.Interfaces.User;
using MVC.Domain.Models;
using MVC.Domain.ViewModels;

namespace MVC.Presentation.Controllers;
    public class UserController : Controller
    {
    public readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;  
    }
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel Model)
     {
      if (ModelState.IsValid)
       {
          var result = await _userService.Login(Model);
            if(result.IsSuccess ==true)
              return RedirectToAction("Index", "Product");
      }
        return View(Model);
     }
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel Model)
     {
        if (ModelState.IsValid)
        {
           var result = await _userService.Register(Model);
              if(result.IsSuccess ==true)
               return RedirectToAction("Index", "Product");
        }
        return View(Model);
     }
    [Route("/SignOutAsync")]
    public async Task<IActionResult> SignOutAsync()
    {
        await _userService.SignOutAsync();
        return RedirectToAction("Register", "User");
    }


}

