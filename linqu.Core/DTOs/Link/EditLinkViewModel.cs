﻿

using System.ComponentModel.DataAnnotations;

namespace linqu.Core.DTOs.Link
{
    public class EditLinkViewModel
    {
        [Key]
        public int LinkId { get; set; }


        [Display(Name = "صاحب لینک")]
        public int LinkOwnerId { get; set; }


        [Display(Name = "عنوان لینک")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string LinkTitle { get; set; }


        [Display(Name = "آدرس لینک")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(2000, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string LinkAddress { get; set; }


        [Display(Name = "کلید دسترسی")]
        [Required]
        public string ShortKey { get; set; }


        [Display(Name = "توضیحات")]
        [MaxLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Description { get; set; }
    }
}
