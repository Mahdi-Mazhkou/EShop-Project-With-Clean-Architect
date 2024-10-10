using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Models
{
    public class Order : BaseEntity
    {
        public int UserId { get; set; }
        public bool IsFinally { get; set; }
        public List<OrderDetail>? OrderDetails { get; set; }
    }
}
