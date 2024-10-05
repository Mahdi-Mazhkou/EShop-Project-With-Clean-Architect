using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Display(Name = "کد تایید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]

        public string ConfirmCode { get; set; }

        [Display(Name = "کلمه عبور جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = " تکرار کلمه عبور جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "کلمه های عبور مغایرت دارند")]
        public string RePassword { get; set; }
    }

    public enum ResetPasswordResult
    {
        Success,
        UserNotFound
    }
}
