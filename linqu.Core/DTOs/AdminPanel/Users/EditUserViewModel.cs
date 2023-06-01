﻿
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;


namespace linqu.Core.DTOs.AdminPanel
{
    public class EditUserViewModel
    {
        [Key]
        public int UserId { get; set; }


        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string FullName { get; set; }



        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        public string Email { get; set; }



        [Display(Name = "کلمه عبور")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [DataType(DataType.Password)]
        public string Password { get; set; }



        [Display(Name = "تکرار کلمه ی عبور")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "کلمه های عبور باهم مغایرت دارند")]
        public string ConfirmPassword { get; set; }



        [Display(Name = "تصویر آواتار")]
        public string UserAvatar { get; set; }



        [Display(Name = "فایل ورودی آواتار")]
        public IFormFile UserAvatarFile { get; set; }


        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }

    }
}
