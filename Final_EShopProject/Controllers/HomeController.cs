using EShop.Infra.Data.Context;
using Final_EShopProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Final_EShopProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyEShopContext _context; 

        public HomeController(ILogger<HomeController> logger, MyEShopContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(int pageId=1)
        {
            int take = 1;
            int skip = (pageId - 1) * take;
            int pageCount = _context.Products.Count() / take;
            ViewBag.PageCount=pageCount;
            var products= _context.Products.Include(x=>x.ProductImages).OrderByDescending(x=>x.CreateDate).Skip(skip).Take(take);
            ViewBag.PageId=pageId;
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
