using EShop.Application.Extentions;
using EShop.Application.Services.Interfaces;
using EShop.Domain.ViewModels.User;
using EShop.Infra.Data.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Final_EShopProject.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly MyEShopContext _context;

        public UserController(IUserService userService, MyEShopContext context)
        {
            _userService = userService;
            _context = context;
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
            var result = await _userService.ChangePassword(model);

            switch (result)
            {
                case ChangePasswordResult.Success:
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

        #region Show Shop Cart
        public IActionResult ShowShopCart()
        {
            var userId = User.GetUserId();
            var order = _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(d => d.Product)
                .ThenInclude(p => p.ProductImages)
                .FirstOrDefault(o => o.UserId == userId && !o.IsFinally);
            return View(order);
        }
        #endregion
        #region Delete From Cart
        public IActionResult DeleteFromCart(int id)
        {
            var userId = User.GetUserId();
            var order = _context.Orders
                .FirstOrDefault(o => o.UserId == userId && !o.IsFinally);
            var detail = _context.OrderDetails.FirstOrDefault(x => x.OrderId == order.Id && x.ProductId == id);
            _context.OrderDetails.Remove(detail);
            _context.SaveChanges();

            return RedirectToAction("ShowShopCart");
            
        }

        #endregion


    }
}
