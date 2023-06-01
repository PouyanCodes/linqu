
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace linqu.DataLayer.Entities.Users
{
    public class Permission
    {
        [Key]
        public int PermissionId { get; set; }


        [Display(Name = "عنوان نقش")]
        [Required]
        [MaxLength(200)]
        public string PermissionTitle { get; set; }


        public int? ParentID { get; set; }


        #region Relations

        [ForeignKey("ParentID")]
        public List<Permission> Permissions { get; set; }

        public List<RolePermission> RolePermissions { get; set; }

        #endregion

    }
}
