
using System.ComponentModel.DataAnnotations;

namespace linqu.Core.DTOs.Account
{
    public class ResetPasswordViewModel
    {       
        [Display(Name = "کد فعال سازی")]
        public string ConfirmationCode { get; set; }


        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} خود را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Display(Name = "تکرار کلمه ی عبور")]
        [Required(ErrorMessage = "لطفا {0} خود را وارد کنید")]
        [MaxLength(50,ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]    
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "کلمه های عبور باهم مغایرت دارند")]
        public string ConfirmPassword { get; set; }
    }
}
