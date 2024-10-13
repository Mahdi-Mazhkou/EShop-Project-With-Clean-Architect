using EShop.Application.Services.Interfaces;
using EShop.Domain.ViewModels.Zarinpal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Services.Implementations
{
    public class SandBoxZarinpalPaymentService : IZarinpalPaymentService
    {
        public ZarinpalCreatePaymentResultViewModel CreatePaymentRequest(ZarinpalCreatePaymentViewModel model)
        {
            var payment = new ZarinpalSandbox.Payment(model.Amount);

            var result = payment.PaymentRequest(model.Description, model.CallbackUrl, model.Email, model.Mobile).Result;

            if (result.Status == (int)PaymentStatus.St100)
            {
                string redirectUrl = "https://sandbox.zarinpal.com/pg/StartPay/" + result.Authority;

                return new ZarinpalCreatePaymentResultViewModel
                {
                    RedirectUrl = redirectUrl,
                    Status = PaymentStatus.St100
                };
            }

            return new ZarinpalCreatePaymentResultViewModel
            {
                RedirectUrl = null,
                Status = (PaymentStatus)result.Status
            };
        }

        public PaymentVerificationResultViewModel PaymentVerification(PaymentVerificationViewModel model)
        {
            var payment = new ZarinpalSandbox.Payment(model.Amount);

            var result = payment.Verification(model.Authority).Result;

            return new PaymentVerificationResultViewModel()
            {
                RefId = result.RefId,
                Status = (PaymentStatus)result.Status
            };
        }
    }
}
