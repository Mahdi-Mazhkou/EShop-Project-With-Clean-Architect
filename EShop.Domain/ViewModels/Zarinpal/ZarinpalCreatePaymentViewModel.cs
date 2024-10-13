using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.ViewModels.Zarinpal
{
    public  class ZarinpalCreatePaymentViewModel
    {
        public string MerchantId { get; set; }

        public int Amount { get; set; }

        public string CallbackUrl { get; set; }

        public string Description { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }
    }
}
