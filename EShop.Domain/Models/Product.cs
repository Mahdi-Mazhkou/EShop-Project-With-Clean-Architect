using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Models
{
    public class Product : BaseEntity
    {
        public int GroupId { get; set; }

        [ForeignKey("GroupId")]
        public ProductGroup? ProductGroup { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Tags { get; set; }
        public List<ProductImage>? ProductImages { get; set; }
    }
}
