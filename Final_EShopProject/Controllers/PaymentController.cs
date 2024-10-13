using EShop.Application.Services.Interfaces;
using EShop.Infra.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using EShop.Domain.ViewModels.Zarinpal;
using System;

namespace Final_EShopProject.Controllers
{
    public class PaymentController : Controller
    {
        private readonly MyEShopContext _context;
        private readonly IZarinpalPaymentService _service;

        public PaymentController(MyEShopContext context, IZarinpalPaymentService service)
        {
            _context = context;
            _service = service;
        }

        public IActionResult StartPay(int orderId)
        {
            var order = _context.Orders
                .Include(x => x.OrderDetails)
                .FirstOrDefault(x => x.Id == orderId);

            if (order == null || order.OrderDetails == null)
            {
                return NotFound();
            }

            string callbackUrl = Url.Action("VerifyPayment", new { Id = orderId });
            var amount = order.OrderDetails.Sum(o => o.Count * o.Price);
            var result = _service.CreatePaymentRequest(new ZarinpalCreatePaymentViewModel
            {
                Amount = amount,
                CallbackUrl = $"https://localhost:7152{callbackUrl}",
                Email = "Mahdi.Mazhkou@yahoo.com",
                Mobile = "09197738968",
                Description = "پرداخت مربوط به سفارش",
                MerchantId = ""

            });

            if (result != null && result.Status == PaymentStatus.St100)
            {
                return Redirect(result.RedirectUrl);
            }

            return View();
        }
        public IActionResult VerifyPayment(string authority, string status, int orderId)
        {
            if (authority.Length == 36 && status.ToLower() == "ok")
            {
                var order = _context.Orders
              .Include(o => o.OrderDetails)
              .FirstOrDefault(o => o.Id == orderId);

                if (order == null || order.OrderDetails == null)
                {
                    return NotFound();
                }

                var amount = order.OrderDetails.Sum(o => o.Count * o.Price);

                var result = _service.PaymentVerification(new PaymentVerificationViewModel()
                {
                    Amount = amount,
                    Authority = authority,
                    MerchantId = ""
                });

                if (result.Status == PaymentStatus.St100)
                {
                    order.IsFinally = true;

                    _context.Orders.Update(order);
                    _context.SaveChanges();

                    return View("SuccessPayment");
                }
                else
                {
                    return View("ErrorPayment");
                }
            }
            return View();
        }

    }
}
