using EShop.Application.Extentions;
using EShop.Application.Services.Interfaces;
using EShop.Domain.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Final_EShopProject.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        #region Change Password
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            #region Validations
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.UserId = User.GetUserId();
            var result= await _userService.ChangePassword(model);

            switch (result)
            {
                case  ChangePasswordResult.Success:
                    return RedirectToAction(nameof(ChangePassword));

                case ChangePasswordResult.CurrentPasswordNotCorrect:
                    ModelState.AddModelError(nameof(ChangePasswordViewModel.CurrentPassword), "کلمه عبور فعلی صحیح نمی باشد.");
                    break;

                case ChangePasswordResult.UserNotFound:
                    ModelState.AddModelError(nameof(ChangePasswordViewModel.CurrentPassword), "کاربر پیدا نشد.");
                    break;
            }
            #endregion
            return View(model);
        }
        #endregion

    }
}
