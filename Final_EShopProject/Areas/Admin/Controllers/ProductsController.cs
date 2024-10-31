using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EShop.Domain.Models;
using EShop.Infra.Data.Context;
using Microsoft.AspNetCore.Authorization;
using EShop.Application.Services.Interfaces;
using EShop.Domain.ViewModels.Product;

namespace Final_EShopProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly MyEShopContext _context;
        private readonly IProductService _service;

        public ProductsController(MyEShopContext context, IProductService service)
        {
            _context = context;
            _service = service;
        }

        #region List Of Products
        public async Task<IActionResult> Index()
        {
            var products = await _service.GetListOfProducts();
            return View(products);

        }
        public async Task<IActionResult> Details(int id)
        {

            var product = await _service.GetProductDetailsByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        #endregion

        #region Create Product
        public async Task<IActionResult> Create()
        {
            var products = await _service.GetListOfProducts();
            var productGroup = products.Select(x => x.ProductGroup);
            ViewData["GroupId"] = new SelectList(_context.ProductGroups, "Id", "GroupTitle");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductViewModel product, IFormFile[] imgUp)
        {
            if (!ModelState.IsValid)
            {
                ViewData["GroupId"] = new SelectList(_context.ProductGroups, "Id", "GroupTitle", product.GroupId);
                return View(product);
            }

            var p = await _service.CreateProductAsync(product, imgUp);
            switch (p)
            {
                case ResultCreateProduct.Success:
                    return RedirectToAction(nameof(Index));
                case ResultCreateProduct.Failed:
                    {
                        ViewData["GroupId"] = new SelectList(_context.ProductGroups, "Id", "GroupTitle", product.GroupId);
                        return View(product);
                    }
                default:
                    {
                        ViewData["GroupId"] = new SelectList(_context.ProductGroups, "Id", "GroupTitle", product.GroupId);
                        return View(product);
                    }
            }

        }
        #endregion

        #region Edit Product
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _service.GetProductByIdForEditAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            ViewData["GroupId"] = new SelectList(_context.ProductGroups, "Id", "GroupTitle", product.GroupId);
            ViewBag.Images = _service.GetProductImagesAsync(product.Id);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditProductViewModel product, IFormFile[] imgUp)
        {
            if (!ModelState.IsValid)
            {
                ViewData["GroupId"] = new SelectList(_context.ProductGroups, "Id", "GroupTitle", product.GroupId);
                return View(product);
            }

            var result = await _service.UpdateProductAsync(product.Id, product, imgUp);
            switch (result)
            {
                case ResultEditProduct.Success:
                    return RedirectToAction(nameof(Index));
                case ResultEditProduct.Failed:
                    {
                        ViewData["GroupId"] = new SelectList(_context.ProductGroups, "Id", "GroupTitle", product.GroupId);
                        return View(product);
                    }

                default:
                    {
                        ViewData["GroupId"] = new SelectList(_context.ProductGroups, "Id", "GroupTitle", product.GroupId);
                        return View(product);
                    }

            }

        }


        #endregion

        #region Delete Product
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _service.GetProductByIdForDeleteAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(DeleteProductViewModel model)
        {

            var result = await _service.DeleteProductAsync(model.Id);

            switch (result)
            {
                case ResultDeleteProduct.Success:
                    return RedirectToAction(nameof(Index));
                case ResultDeleteProduct.Failed:
                    {
                        ModelState.AddModelError(nameof(DeleteProductViewModel), "عملیات با شکست مواجه شد");
                        return View(model);
                    }
                default: return View(model);
            }

        }
        public void DeleteImage(int id)
        {
            var img = _context.ProductImages.Find(id);
            if (img != null)
            {
                _context.ProductImages.Remove(img);
                string deletePath = Path.Combine(Directory.GetCurrentDirectory(),
                              "wwwroot/Images", img.ImageName);
                if (System.IO.File.Exists(deletePath))
                {
                    System.IO.File.Delete(deletePath);
                }
                _context.SaveChanges();
            }

        }
        #endregion

    }
}
