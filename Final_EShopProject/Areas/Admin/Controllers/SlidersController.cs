using EShop.Domain.Models;
using EShop.Infra.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace Final_EShopProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlidersController : Controller
    {
        private readonly MyEShopContext _context;
        public SlidersController(MyEShopContext context)
        {
                _context= context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sliders.ToListAsync());
        }
        public  IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Slider slider,IFormFile imgUp)
        {
            slider.ImageName = imgUp.Name;
            
            if(ModelState.IsValid)
            {
                if (imgUp == null)
                    return View();
                var x = Path.GetExtension(imgUp.FileName);
                string imageName = Guid.NewGuid().ToString() + Path.GetExtension(imgUp.FileName);
                string savePath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot/Sliders", imageName);
                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    imgUp.CopyTo(stream);
                }
                slider.ImageName = imageName;
                slider.IsDelete = false;
                slider.CreateDate = DateTime.Now;
                _context.Add(slider);
               await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
           
        }
        
    }
}
