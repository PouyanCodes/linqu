

using System.ComponentModel.DataAnnotations;

namespace linqu.Core.DTOs.Link
{
    public class CreateLinkViewModel
    {
        [Display(Name = "صاحب لینک")]
        public int LinkOwnerId { get; set; }


        [Display(Name = "عنوان لینک")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string LinkTitle { get; set; }


        [Display(Name = "آدرس لینک")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(2000, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string LinkAddress { get; set; }


        [Display(Name = "کلید دسترسی")]
        public string ShortKey { get; set; }


        [Display(Name = "توضیحات")]
        [MaxLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Description { get; set; }
    }
}
