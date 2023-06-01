
using System;
using System.ComponentModel.DataAnnotations;


namespace linqu.DataLayer.Entities.Users
{
    public class UserInformation
    {
        [Key]
        public int UserId { get; set; }


        [Required]
        [Display(Name ="نام و نام خانوادگی")]
        [MaxLength(200)]
        public string FullName { get; set; }
  

        #region Relations

        public virtual User User { get; set; }

        #endregion


    }
}
