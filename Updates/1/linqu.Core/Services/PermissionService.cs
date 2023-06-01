
using linqu.Core.Interfaces;
using linqu.DataLayer.Context;
using linqu.DataLayer.Entities.Users;
using System.Collections.Generic;
using System.Linq;

namespace linqu.Core.Services
{
    public class PermissionService : IPermissionService
    {

        private readonly DataBaseContext _context;

        public PermissionService(DataBaseContext context)
        {
            _context = context;
        }

        public void AddPermissionsToRole(int roleId, List<int> permissions)
        {
            foreach (var permission in permissions)
            {
                _context.RolePermissions.Add(new RolePermission()
                {
                    PermissionId = permission,
                    RoleId = roleId
                });
            }

            _context.SaveChanges();
        }

        public void EditRolePermissions(int roleId, List<int> permissions)
        {
            // Delete All Role Permissions

            _context.RolePermissions.Where(p => p.RoleId == roleId)
               .ToList().ForEach(p => _context.RolePermissions.Remove(p));

            // Add New Permissions

            AddPermissionsToRole(roleId, permissions);
        }

        public List<Permission> GetPermissions()
        {
            return _context.Permissions.ToList();
        }

        public List<int> GetRolePermissions(int roleId)
        {
            return _context.RolePermissions
                .Where(r => r.RoleId == roleId)
                .Select(r => r.PermissionId).ToList();
        }

        public bool CheckPermission(int permissionId, string email)
        {
            int userId = _context.Users.Single(u => u.Email == email).UserId;

            List<int> UserRoles = _context.UserRoles
                .Where(r => r.UserId == userId).Select(r => r.RoleId).ToList();

            if (!UserRoles.Any())
                return false;

            List<int> RolesPermission = _context.RolePermissions
                .Where(p => p.PermissionId == permissionId)
                .Select(p => p.RoleId).ToList();

            return RolesPermission.Any(p => UserRoles.Contains(p));
        }
    }
}
