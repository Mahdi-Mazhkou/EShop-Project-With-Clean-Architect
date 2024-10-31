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
    public  class ListOfProductViewModel
    {
        public int Id { get; set; }
        public int GroupId { get; set; }

        [Display(Name = "عنوان گروه")]
        [ForeignKey("GroupId")]
        public ProductGroup? ProductGroup { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }

        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Price { get; set; }

        [Display(Name = "تاریخ ایجاد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public DateTime CreateDate { get; set; }
        [Display(Name = "پاک شود؟")]
        public bool IsDelete { get; set; }


    }
}
