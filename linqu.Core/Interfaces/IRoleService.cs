
using linqu.Core.DTOs.AdminPanel;
using linqu.Core.DTOs.AdminPanel.Roles;
using linqu.DataLayer.Entities.Users;
using System.Collections.Generic;


namespace linqu.Core.Interfaces
{
    public interface IRoleService
    {
        int AddRole(Role role);
        int AddRole(CreateRoleViewModel role);
        void UpdateRole(Role role);
        void EditRole(EditRoleViewModel role);  
        void DeleteRole(Role role);
        void DeleteRole(int roleId);



        List<RoleDataViewModel> GetRoles();
        Role GetRoleById(int roleId);
        string GetRoleTitleById(int roleId);
        List<int> GetUsersByRole(int roleId);
        EditRoleViewModel GetRoleDataForEdit(int roleId);
        int GetRoleMagnitudeOrder(int roleId);



        void AddRoleToUsers(int roleId, List<int> userIds);
        void AddRoleToUser(int userId, int roleId);
        void AddRolesToUser(int userId, List<int> roleIds);      
        void EditUserRoles(int userId, List<int> rolesId);
        void EditMultipleUsersRole(List<int> userIds,int oldRoleId , int newRoleId);



        bool IsRoleExist(string roleTitle);
        bool IsGreatAdmin(int roleId);
        bool MatchRoleTitleWithId(int roleId, string roleTitle);     
    }
}
