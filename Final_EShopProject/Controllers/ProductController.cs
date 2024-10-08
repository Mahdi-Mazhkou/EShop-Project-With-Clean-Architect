using EShop.Infra.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Final_EShopProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly MyEShopContext _context;

        public ProductController(MyEShopContext context)
        {
            _context = context;
        }

        public IActionResult Index(string q)
        {
            var product = _context.Products.Include(p => p.ProductImages)
                .Where(p => p.Title.Contains(q) || p.Description.Contains(q) ||
                p.Tags.Contains(q));
            return View(product);
        }

        [Route("Group/{id}/{title}")]
        public IActionResult ShowProductsByGroup(int id, string title)
        {
            var productByGroup = _context.Products.Include(x => x.ProductImages).Where(x => x.GroupId == id);
            return View(productByGroup);
        }
        public IActionResult ShowProduct(int id)
        {
            var product = _context.Products.Include(x => x.ProductImages).FirstOrDefault(x => x.Id == id);
            return View(product);
        }
    }
}
