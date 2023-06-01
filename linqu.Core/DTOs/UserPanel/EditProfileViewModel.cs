

using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;


namespace linqu.Core.DTOs.UserPanel
{
    public class EditProfileViewModel
    {
        [Key]
        public int UserId { get; set; }


        [Display(Name = "تصویر آواتار")]
        [MaxLength(200)]
        public string UserAvatar { get; set; }


        [Display(Name = "فایل ورودی آواتار ")]
        public IFormFile UserAvatarFile { get; set; }



        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string FullName { get; set; }
    }
}
