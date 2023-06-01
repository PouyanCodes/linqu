
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace linqu.DataLayer.Entities.Users
{
   public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Display(Name = "عنوان نقش")]
        [Required]
        [MaxLength(200)]
        public string RoleTitle { get; set; }


        [Display(Name = "میزان بزرگی نقش")]
        [Required]  
        public int OrderOfMagnitude { get; set; }


        [Required]
        public bool IsDeleted { get; set; }


        #region Relations

        public virtual List<UserRole> UserRoles { get; set; }
        public List<RolePermission> RolePermissions { get; set; }


        #endregion
    }
}
