using MVC.Domain.Helper;
using MVC.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Application.Services.Interfaces.User;
    public interface IUserService
    {
    Task<ResponseResult> Login(LoginViewModel model);
    Task<ResponseResult> Register(RegisterViewModel model);
    Task<ResponseResult> SignOutAsync();
    }

