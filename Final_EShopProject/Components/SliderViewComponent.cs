using EShop.Infra.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Final_EShopProject.Components
{
    public class SliderViewComponent : ViewComponent
    {
        private readonly MyEShopContext context;

        public SliderViewComponent(MyEShopContext context)
        {
            this.context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sliders = context.Sliders
                .Where(s => s.StartDate <= DateTime.Now && s.EndDate >= DateTime.Now)
                .ToList();
            return View(sliders);
        }
    }
}
