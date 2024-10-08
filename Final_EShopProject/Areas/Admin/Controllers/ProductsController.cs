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

namespace Final_EShopProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly MyEShopContext _context;

        public ProductsController(MyEShopContext context)
        {
            _context = context;
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {
            var myEShopContext = _context.Products.Include(p => p.ProductGroup);
            return View(await myEShopContext.ToListAsync());
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.ProductGroup)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            ViewData["GroupId"] = new SelectList(_context.ProductGroups, "Id", "GroupTitle");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, IFormFile[] imgUp)
        {

            if (ModelState.IsValid)
            {
                product.CreateDate = DateTime.Now;
                _context.Add(product);
                _context.SaveChanges();

                if (imgUp != null && imgUp.Any())
                {
                    foreach (var img in imgUp)
                    {
                        string imageName = Guid.NewGuid().ToString() + Path.GetExtension(img.FileName);
                        string savePath = Path.Combine(Directory.GetCurrentDirectory(),
                            "wwwroot/Images", imageName);
                        using (var stream = new FileStream(savePath, FileMode.Create))
                        {
                            img.CopyTo(stream);
                        }

                        _context.ProductImages.Add(new ProductImage()
                        {
                            CreateDate = DateTime.Now,
                            IsDelete = false,
                            ImageName = imageName,
                            ProductId = product.Id,
                        });
                    }
                    _context.SaveChanges();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupId"] = new SelectList(_context.ProductGroups, "Id", "GroupTitle", product.GroupId);
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["GroupId"] = new SelectList(_context.ProductGroups, "Id", "GroupTitle", product.GroupId);
            ViewBag.Images = _context.ProductImages.Where(x => x.ProductId == id.Value).ToList();
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product, IFormFile[] imgUp)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);

                    if (imgUp != null && imgUp.Any())
                    {
                        foreach (var img in imgUp)
                        {
                            string imageName = Guid.NewGuid().ToString() + Path.GetExtension(img.FileName);
                            string savePath = Path.Combine(Directory.GetCurrentDirectory(),
                                "wwwroot/Images", imageName);
                            using (var stream = new FileStream(savePath, FileMode.Create))
                            {
                                img.CopyTo(stream);
                            }

                            _context.ProductImages.Add(new ProductImage()
                            {
                                CreateDate = DateTime.Now,
                                IsDelete = false,
                                ImageName = imageName,
                                ProductId = product.Id,
                            });
                        }
                    }
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupId"] = new SelectList(_context.ProductGroups, "Id", "GroupTitle", product.GroupId);
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.ProductGroup)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                product.IsDelete = true;
                _context.Products.Update(product);
                var images = _context.ProductImages.Where(i => i.ProductId == id);
                foreach (var image in images)
                {
                    string deletePath = Path.Combine(Directory.GetCurrentDirectory(),
                                                  "wwwroot/Images", image.ImageName);
                    if (System.IO.File.Exists(deletePath))
                    {
                        System.IO.File.Delete(deletePath);
                    }
                    _context.ProductImages.Remove(image);
                }
            }

             _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
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
    }
}
