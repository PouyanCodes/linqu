
using System.ComponentModel.DataAnnotations;


namespace linqu.Core.DTOs.Account
{
    public class AccountActivationViewModel
    {
        [Display(Name = "نام و نام خانوادگی")]
        [Required]     
        public string FullName { get; set; }

        
        [Display(Name = "ایمیل")]
        [Required]
        public string Email { get; set; }

        
        [Display(Name = "کد فعال سازی")]
        [Required]
        public string ConfirmationCode { get; set; }
    }
}
