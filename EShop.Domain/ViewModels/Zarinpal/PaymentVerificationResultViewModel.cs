using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.ViewModels.Zarinpal
{
    public  class PaymentVerificationResultViewModel
    {
        public long RefId { get; set; }

        public PaymentStatus Status { get; set; }
    }
}
