using EShop.Domain.Models;
using EShop.Infra.Data.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;

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

        [Authorize]
        public IActionResult AddToCart(int id)
        {
            string emailUser = User.FindFirstValue(ClaimTypes.Email);
            int currentUserId = _context.Users.FirstOrDefault(u => u.UserEmail == emailUser).Id;

            Order order = _context.Orders.FirstOrDefault(o => o.UserId == currentUserId && !o.IsFinally);
            if (order == null)
            {
                order = new Order()
                {
                    UserId = currentUserId,
                    IsDelete = false,
                    CreateDate = DateTime.Now,
                };
                _context.Orders.Add(order);
                _context.SaveChanges();
            }

            OrderDetail orderDetail = _context.OrderDetails
                .FirstOrDefault(d => d.OrderId == order.Id && d.ProductId == id);
            if (orderDetail != null)
            {
                orderDetail.Count += 1;
            }
            else
            {
                orderDetail = new OrderDetail()
                {
                    Count = 1,
                    IsDelete = false,
                    CreateDate = DateTime.Now,
                    Price = _context.Products.Find(id).Price,
                    OrderId = order.Id,
                    ProductId = id,
                };
                _context.OrderDetails.Add(orderDetail);
            }
            _context.SaveChanges();
            var headers = Request.GetTypedHeaders();
            var referer = headers.Referer;
            var refererRelativePath = referer?.PathAndQuery;
            return Redirect(refererRelativePath);
        }
        public int CountShopCart()
        {
            if (User.Identity.IsAuthenticated)
            {
                string emailUser = User.FindFirstValue(ClaimTypes.Email);
                int currentUserId = _context.Users.FirstOrDefault(u => u.UserEmail == emailUser).Id;
                int? orderid = _context.Orders.FirstOrDefault(o => o.UserId == currentUserId && !o.IsFinally)?.Id;
                if (orderid != null)
                {
                    {
                        return _context.OrderDetails.Where(o => o.OrderId == orderid.Value)
                    .Sum(d => d.Count);
                    }

                }
            }
            return 0;
        }
    }
}
