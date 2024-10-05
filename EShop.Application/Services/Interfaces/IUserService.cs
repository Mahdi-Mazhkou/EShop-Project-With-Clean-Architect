using EShop.Domain.Models;
using EShop.Domain.ViewModels;
using EShop.Domain.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<ResultRegister> CreateAsync(RegisterViewModel model);
        Task<ResultLogin> LoginUser(LoginViewModel login);
        Task<User> GetUserByEmail(string email);
        Task<ResultForgotPassword> ForgotPassword(ForgotPasswordViewModel forgot);
        Task<ChangePasswordResult> ChangePassword(ChangePasswordViewModel change);
        Task<ResetPasswordResult> ResetPassword(ResetPasswordViewModel reset);
    }
}
