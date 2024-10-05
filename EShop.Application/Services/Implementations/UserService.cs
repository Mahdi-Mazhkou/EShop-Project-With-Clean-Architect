﻿using EShop.Application.Extentions;
using EShop.Application.Generators;
using EShop.Application.Services.Interfaces;
using EShop.Domain.Interfaces;
using EShop.Domain.Models;
using EShop.Domain.ViewModels;
using EShop.Domain.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailSender _emailSender;
        public UserService(IUserRepository userRepository, IEmailSender emailSender)
        {
            _userRepository = userRepository;
            _emailSender = emailSender;
        }

        public async Task<ChangePasswordResult> ChangePassword(ChangePasswordViewModel change)
        {
            if (!change.UserId.HasValue)
            {
                return ChangePasswordResult.UserNotFound;
            }
            var user = await _userRepository.GetUserByIdAsync(change.UserId.Value);
            if (user == null)
            {
                return ChangePasswordResult.UserNotFound;
            }
            if (user.Password != change.Password.EncodePasswordMd5())
            {
                return ChangePasswordResult.CurrentPasswordNotCorrect;
            }

            user.Password = change.Password.EncodePasswordMd5();
            _userRepository.Update(user);
            await _userRepository.SaveAsync();

            return ChangePasswordResult.Success;
        }

        public async Task<ResultRegister> CreateAsync(RegisterViewModel model)
        {
            try
            {
                if (await _userRepository.IsUserExistsByEmail(model.UserEmail.Trim().ToLower()))
                {
                    return ResultRegister.EmailInValid;
                }

                User user = new User()
                {
                    UserName = model.UserName,
                    UserEmail = model.UserEmail.Trim().ToLower(),
                    Password = model.Password.EncodePasswordMd5(),
                    IsDelete = false,
                    IsAdmin = false,
                    CreateDate = DateTime.Now
                };

                await _userRepository.InsertAsync(user);
                await _userRepository.SaveAsync();

                return ResultRegister.Success;
            }
            catch
            {

                return ResultRegister.Faild;
            }

        }

        public async Task<ResultForgotPassword> ForgotPassword(ForgotPasswordViewModel forgot)
        {
            string email = forgot.UserEmail.Trim().ToLower();
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null)
            {
                return ResultForgotPassword.UserNotFound;
            }

            var uniqueCode = CodeGenerators.GetUniqueCode();
            user.ConfirmCode = uniqueCode;
            _userRepository.Update(user);
            await _userRepository.SaveAsync();

            string body = $"<h1>کد تایید شما: {user.ConfirmCode}</h1>";
            _emailSender.SendEmail(user.UserEmail, "کد تایید", body);

            return ResultForgotPassword.Success;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userRepository.GetUserByEmail(email.Trim().ToLower());
        }

        public async Task<ResultLogin> LoginUser(LoginViewModel login)
        {
            login.UserEmail = login.UserEmail.Trim().ToLower();
            User user = await _userRepository.GetUserByEmail(login.UserEmail);
            if (user == null)
            {
                return ResultLogin.UserNotFound;
            }
            if (user.Password != login.Password.EncodePasswordMd5())
            {
                return ResultLogin.UserNotFound;
            }
            return ResultLogin.Success;

        }

        public async Task<ResetPasswordResult> ResetPassword(ResetPasswordViewModel reset)
        {
            var user = await _userRepository.GetUserByConfirmCode(reset.ConfirmCode);
            if (user == null)
            {
                return ResetPasswordResult.UserNotFound;
            }
            user.Password = reset.Password.EncodePasswordMd5();
            user.ConfirmCode = null;
            _userRepository.Update(user);
            await _userRepository.SaveAsync();
            return ResetPasswordResult.Success;
        }
    }
}