using EShop.Domain.ViewModels.Zarinpal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Services.Interfaces
{
    public interface IZarinpalPaymentService
    {
        ZarinpalCreatePaymentResultViewModel CreatePaymentRequest(ZarinpalCreatePaymentViewModel model);
        PaymentVerificationResultViewModel PaymentVerification(PaymentVerificationViewModel model);
    }
}
