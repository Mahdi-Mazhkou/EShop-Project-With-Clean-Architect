using EShop.Application.Services.Interfaces;
using EShop.Domain.Models;
using EShop.Domain.ViewModels;
using EShop.Infra.Data.Context;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Security.Claims;

namespace Final_EShopProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        #region Register
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            #region Validations
            if (!ModelState.IsValid)
            {
                return View(register);
            }

            #endregion

            var result = await _userService.CreateAsync(register);

            switch (result)
            {
                case ResultRegister.Success:
                    return View("SuccessRegister", register);

                case ResultRegister.Faild:
                    return View(register);

                case ResultRegister.EmailInValid:
                    {
                        ModelState.AddModelError("UserEmail", "ایمیل وارد شده از قبل موجود است.");
                        return View(register);
                    }
                default: return View(register);

            }


        }

        #endregion

        #region Login
        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            #region Validations
            if (!ModelState.IsValid)
                return View(login);
            #endregion

            var result = await _userService.LoginUser(login);
            switch (result)
            {
                case ResultLogin.Success:
                    var user = await _userService.GetUserByEmail(login.UserEmail);
                    if (user == null)
                    {
                        ModelState.AddModelError("UserEmail", "اطلاعات وارد شده صحیح نمیباشد.");
                        return View(login);
                    }
                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                        new Claim(ClaimTypes.Email,user.UserEmail),
                        new Claim("UserName",user.UserName)
                    };
                    var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimIdentity);
                    var authenticationProperties = new AuthenticationProperties()
                    {
                        IsPersistent = true,
                    };

                    await HttpContext.SignInAsync(claimsPrincipal, authenticationProperties);
                    return Redirect("/");

                case ResultLogin.UserNotFound:
                    {
                        ModelState.AddModelError("UserEmail", "اطلاعات وارد شده صحیح نمیباشد.");
                        return View(login);
                    }
            }
            return View();
        }
        #endregion

        #region Logout
        [HttpGet("/logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }
        #endregion

        #region ForgotPassword
        [HttpGet("/Forgot-Password")]
        public async Task<IActionResult> ForgotPassword()
        {
            return View();
        }

        [HttpPost("/Forgot-Password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgot)
        {
            var result = await _userService.ForgotPassword(forgot);

            switch (result)
            {
                case ResultForgotPassword.Success:
                    return RedirectToAction(nameof(RestPassword));

                case ResultForgotPassword.UserNotFound:
                    ModelState.AddModelError(nameof(ForgotPasswordViewModel.UserEmail), "مشخصات وارد شده صحیح نمی باشد.");
                    break;
            }
            return View();
        }
        #endregion
        #region Reset Password

        [HttpGet("/Reset-Password")]
        public async Task<IActionResult> RestPassword()
        {
            return View();
        }

        [HttpPost("/Reset-Password")]
        public async Task<IActionResult> RestPassword(ResetPasswordViewModel model)
        {
            #region Validations
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            #endregion
            var result = await _userService.ResetPassword(model);
            switch (result)
            {
                case ResetPasswordResult.Success:
                    return RedirectToAction(nameof(Login));

                case ResetPasswordResult.UserNotFound:
                    return RedirectToAction(nameof(ForgotPassword));
            }
            return View();
        }

        #endregion

    }
}
