using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EShop.Domain.Models;
using EShop.Infra.Data.Context;
using EShop.Application.Extentions;
using EShop.Domain.ViewModels.User;
using EShop.Application.Services.Interfaces;
using EShop.Domain.ViewModels;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace Final_EShopProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        #region :List Of Users
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetListOfUsers());
        }
        #endregion

        #region Details Of User
        public async Task<IActionResult> Details(int id)
        {
            var user = await _service.GetUserDetailsByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        #endregion


        #region Create User
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _service.CreateUserAsync(model);
            switch (result)
            {
                case ResultCreateUser.Success:
                    return RedirectToAction(nameof(Index));

                case ResultCreateUser.Failed:
                    return View(model);

                case ResultCreateUser.EmailInValid:
                    {
                        ModelState.AddModelError("UserEmail", "ایمیل وارد شده از قبل موجود است.");
                        return View(model);
                    }
                default: return View(model);

            }

        }

        #endregion


        #region Edit User
        public async Task<IActionResult> Edit(int id)
        {

            var user = await _service.GetUserForEditAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            var result = await _service.UpdateUserByIdAsync(model);

            switch (result)
            {
                case ResultEditUser.Success:
                    return RedirectToAction(nameof(Index));

                case ResultEditUser.Failed:
                    return View(model);

                case ResultEditUser.UserNotFound:
                    {
                        ModelState.AddModelError("UserName", "کاربری یافت نشد.");
                        return View(model);
                    }
                case ResultEditUser.EmailInValid:
                    {
                        ModelState.AddModelError("UserEmail", "کاربری با ایمیل وارد شده در سیستم وجود دارد.");
                        return View(model);
                    }
                default: return View(model);

            }


        }
        #endregion


        #region Delete User
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _service.GetUserDeleteByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
       
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _service.DeleteUser(id);
            switch (user)
            {
                case ResultDeleteUser.Success:
                    return RedirectToAction(nameof(Index));
                case ResultDeleteUser.Failed:
                    return RedirectToAction(nameof(Delete));
                default: return RedirectToAction(nameof(Delete));
            }

        }
        #endregion

    }
}
