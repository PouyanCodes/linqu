
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace linqu.DataLayer.Entities.Users
{
    public class User
    {
        [Key]
        public int UserId { get; set; }


        [Display(Name = "ایمیل")]
        [Required]
        [MaxLength(200)]
        [EmailAddress]
        public string Email { get; set; }


        [Display(Name = "کلمه عبور")]
        [Required]
        [MaxLength(200)]
        public string Password { get; set; }


        [Display(Name = "کد فعال سازی")]
        [MaxLength(50)]
        public string ConfirmationCode { get; set; }


        [Display(Name = "وضعیت")]
        [Required]
        public bool IsActive { get; set; }


        [Display(Name = "آواتار")]
        [Required]
        [MaxLength(200)]
        public string UserAvatar { get; set; }


        [Display(Name = "تاریخ ثبت نام")]
        [Required]
        public DateTime RegisterDate { get; set; }


        [Required]
        public bool IsDeleted { get; set; }


        #region Relations

        public virtual List<UserRole> UserRoles { get; set; }
        public virtual UserInformation UserInformation { get; set; }

        #endregion


    }
}
