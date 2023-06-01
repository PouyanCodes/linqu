
using System.ComponentModel.DataAnnotations;


namespace linqu.Core.DTOs.Account
{
    public class ForgotPasswordViewModel
    {      
        [Display(Name ="ایمیل")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        [EmailAddress(ErrorMessage ="فرمت {0} شما صحیح نیست ")]
        public string Email { get; set; }
    }
}
