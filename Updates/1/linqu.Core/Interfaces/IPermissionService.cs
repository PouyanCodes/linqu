
using linqu.DataLayer.Entities.Users;
using System.Collections.Generic;


namespace linqu.Core.Interfaces
{
    public interface IPermissionService
    {      
        void AddPermissionsToRole(int roleId, List<int> permissions);
        void EditRolePermissions(int roleId, List<int> permissions);

  

        List<Permission> GetPermissions();
        List<int> GetRolePermissions(int roleId);


        bool CheckPermission(int permissionId, string email);

    }
}
