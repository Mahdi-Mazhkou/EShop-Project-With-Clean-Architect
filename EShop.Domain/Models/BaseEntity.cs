using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Models
{
    public  class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "تاریخ ایجاد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public DateTime CreateDate { get; set; } = DateTime.Now;
        [Display(Name = "پاک شود؟")]
        public bool IsDelete { get; set; }
    }
}
