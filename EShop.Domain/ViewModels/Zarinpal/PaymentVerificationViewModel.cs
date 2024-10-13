using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.ViewModels.Zarinpal
{
    public  class PaymentVerificationViewModel
    {
        public string MerchantId { get; set; }

        public string Authority { get; set; }

        public int Amount { get; set; }
    }
}
