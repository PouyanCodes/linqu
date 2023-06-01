
using linqu.Core.Convertors;
using linqu.Core.DTOs.AdminPanel;
using linqu.Core.DTOs.AdminPanel.Roles;
using linqu.Core.Interfaces;
using linqu.DataLayer.Context;
using linqu.DataLayer.Entities.Users;
using System.Collections.Generic;
using System.Linq;


namespace linqu.Core.Services
{
    public class RoleService : IRoleService
    {
        private readonly DataBaseContext _context;

        public RoleService(DataBaseContext context)
        {
            _context = context;
        }

        public int AddRole(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
            return role.RoleId;
        }

        public int AddRole(CreateRoleViewModel role)
        {
            Role newRole = new Role()
            {
                RoleTitle = role.RoleTitle,
                OrderOfMagnitude = role.OrderOfMagnitude
            };

            return AddRole(newRole); 
        }

        public void UpdateRole(Role role)
        {
            _context.Roles.Update(role);
            _context.SaveChanges();
        }

        public void EditRole(EditRoleViewModel role)
        {
            Role editedRole = new Role()
            {
                RoleId = role.RoleId,
                RoleTitle = role.RoleTitle,
                OrderOfMagnitude = role.OrderOfMagnitude
            };

            UpdateRole(editedRole);
        }

        public void DeleteRole(Role role)
        {
            role.IsDeleted = true;
            UpdateRole(role);
        }

        public void DeleteRole(int roleId)
        {
            var role = _context.Roles.Find(roleId);
            role.IsDeleted = true;
            UpdateRole(role);
        }

        public void AddRoleToUsers(int roleId, List<int> userIds)
        {
            foreach (int userId in userIds)
            {
                _context.UserRoles.Add(new UserRole()
                {
                    RoleId = roleId,
                    UserId = userId
                });
            }

            _context.SaveChanges();
        }

        public void AddRoleToUser(int userId, int roleId)
        {
            _context.UserRoles.Add(new UserRole()
            {
                RoleId = roleId,
                UserId = userId
            });

            _context.SaveChanges();
        }

        public void AddRolesToUser(int userId, List<int> roleIds)
        {
            foreach (int roleId in roleIds)
            {
                _context.UserRoles.Add(new UserRole()
                {
                    RoleId = roleId,
                    UserId = userId
                });
            }

            _context.SaveChanges();
        }

        public void EditUserRoles(int userId, List<int> rolesId)
        {
            // Delete All User Roles

            _context.UserRoles.Where(r => r.UserId == userId).ToList()
               .ForEach(r => _context.UserRoles.Remove(r));

            // Add New Roles

            AddRolesToUser(userId, rolesId);
        }

        public void EditMultipleUsersRole(List<int> userIds, int oldRoleId, int newRoleId)
        {
            // Delete All Users Role

            var usersRole = _context.UserRoles.Where(r => r.RoleId == oldRoleId).ToList();

            foreach (var role in usersRole)
                _context.UserRoles.Remove(role);

            // Add a New Role To Users

            AddRoleToUsers(newRoleId, userIds);
        }

        public List<RoleDataViewModel> GetRoles()
        {
            return _context.Roles.Select(r => new RoleDataViewModel()
            {
                RoleId = r.RoleId,
                RoleTitle = r.RoleTitle,
                OrderOfMagnitude = r.OrderOfMagnitude
            }).ToList();
        }

        public Role GetRoleById(int roleId)
        {
            return _context.Roles.Find(roleId);
        }

        public string GetRoleTitleById(int roleId)
        {
            return _context.Roles.Find(roleId).RoleTitle;
        }

        public List<int> GetUsersByRole(int roleId)
        {
            return _context.UserRoles.Where(r => r.RoleId == roleId).Select(u => u.UserId).ToList();
        }

        public EditRoleViewModel GetRoleDataForEdit(int roleId)
        {
            var role = _context.Roles.Find(roleId);

            return new EditRoleViewModel()
            {
                RoleId = role.RoleId,
                RoleTitle = role.RoleTitle,
                OrderOfMagnitude = role.OrderOfMagnitude
            };
        }

        public int GetRoleMagnitudeOrder(int roleId)
        {
            return _context.Roles.Single(r => r.RoleId == roleId).OrderOfMagnitude;
        }

        public bool IsRoleExist(string roleTitle)
        {
            return _context.Roles.Any(r => r.RoleTitle.Replace(" ", string.Empty).ToLower() == roleTitle.Replace(" ", string.Empty).ToLower());
        }

        public bool IsGreatAdmin(int roleId)
        {
            // The Magnitude Order Of Great Admin is 0 

            if (GetRoleMagnitudeOrder(roleId) == 0)
                return true;
            else
                return false;
        }

        public bool MatchRoleTitleWithId(int roleId, string roleTitle)
        {
            return _context.Roles.Any(r => r.RoleId == roleId && r.RoleTitle == roleTitle);
        }

    }
}
