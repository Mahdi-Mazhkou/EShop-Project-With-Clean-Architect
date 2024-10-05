using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.ViewModels
{
    public  class ForgotPasswordViewModel
    {
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [EmailAddress(ErrorMessage = "فرمت ایمیل صحیح نمیباشد.")]

        public string UserEmail { get; set; }
    }

    public enum ResultForgotPassword
    {
        Success,    
        UserNotFound
    }
}
