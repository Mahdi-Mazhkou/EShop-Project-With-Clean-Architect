using EShop.Infra.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Final_EShopProject.Components
{
    public class ShopCartViewComponent : ViewComponent
    {
        private readonly MyEShopContext context;

        public ShopCartViewComponent(MyEShopContext context)
        {
            this.context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string emailUser = HttpContext.User.FindFirstValue(ClaimTypes.Email);
            int currentUserId = context.Users.FirstOrDefault(u => u.UserEmail == emailUser).Id;

            var order = context.Orders.Include(o => o.OrderDetails).ThenInclude(d => d.Product)
                .ThenInclude(p => p.ProductImages)
                .FirstOrDefault(o => o.UserId == currentUserId && !o.IsFinally);

            return View(order);
        }
    }
}
