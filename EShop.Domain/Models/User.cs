﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Models
{
    public class User:BaseEntity
    {
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
    }
}
