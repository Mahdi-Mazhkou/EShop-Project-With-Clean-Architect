using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.ViewModels.User
{
    public  class EditUserViewModel
    {
        public int Id { get; set; }
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public string UserEmail { get; set; }

        [Display(Name = "ادمین می باشد؟")]
        public bool IsAdmin { get; set; }

    }
    public enum ResultEditUser
    {
        Success,
        Failed,
        UserNotFound,
        EmailInValid
    }
}
