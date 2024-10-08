using EShop.Infra.Data.Context;
using Microsoft.AspNetCore.Mvc;

namespace Final_EShopProject.Components
{
    public class ProductGroupsViewComponent: ViewComponent
    {
        private readonly MyEShopContext _context;
        public ProductGroupsViewComponent(MyEShopContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var list= _context.ProductGroups.Where(x=>!x.IsDelete);
            return View(list);
        }
        
    }
}
