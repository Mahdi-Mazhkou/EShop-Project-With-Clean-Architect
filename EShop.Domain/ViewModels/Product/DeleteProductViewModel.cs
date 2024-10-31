using EShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.ViewModels.Product
{
    public class DeleteProductViewModel
    {
        public int Id { get; set; }

        [Display(Name = "عنوان گروه")]
        public int GroupId { get; set; }

        [Display(Name = "عنوان گروه")]
        [ForeignKey("GroupId")]
        public ProductGroup? ProductGroup { get; set; }

        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Display(Name = "توضیح مختصر")]
        public string ShortDescription { get; set; }

        [Display(Name = "توضیح کامل")]
        public string Description { get; set; }

        [Display(Name = "قیمت")]
        public int Price { get; set; }

        [Display(Name = "کلمات کلیدی")]
        public string Tags { get; set; }

        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreateDate { get; set; }
        

        [Display(Name = "پاک شود؟")]
        public bool IsDelete { get; set; }
    }
    public enum ResultDeleteProduct
    {
        Success,
        Failed
    }
}
