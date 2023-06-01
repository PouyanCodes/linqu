

using linqu.DataLayer.Entities.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace linqu.DataLayer.Entities.Links
{
    public class Link
    {
        [Key]
        public int LinkId { get; set; }


        [Required]
        [Display(Name = "صاحب لینک")]
        public int LinkOwnerId { get; set; }


        [Display(Name = "عنوان لینک")]
        [Required]
        [MaxLength(200)]
        public string LinkTitle { get; set; }


        [Display(Name = "آدرس لینک")]
        [Required]
        [MaxLength(2000)]
        public string LinkAddress { get; set; }


        [Display(Name = "کلید دسترسی")]
        [Required]
        public string ShortKey { get; set; }


        [Display(Name = "توضیحات")]
        [MaxLength(500)]
        public string Description { get; set; }


        [Display(Name = "تاریخ ثبت")]
        [Required]
        public DateTime CreateDate { get; set; }


        [Required]
        public bool IsDeleted { get; set; }


        #region Relations


        [ForeignKey("LinkOwnerId")]
        public User User { get; set; }



        #endregion
    }
}
