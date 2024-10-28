using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.ViewModels.User
{
    public  class DeleteUserViewModel
    {
        public int Id { get; set; }
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public string UserEmail { get; set; }

        [Display(Name = "پسورد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public string Password { get; set; }

        [Display(Name = "ادمین می باشد؟")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public bool IsAdmin { get; set; }

        [Display(Name = "کد تایید")]
        public string? ConfirmCode { get; set; }

        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreateDate { get; set; }
        [Display(Name = "پاک شود؟")]
        public bool IsDelete { get; set; }
    }
   public  enum ResultDeleteUser
    {
        Success,
        Failed,
        UserNotFound
    }
}
