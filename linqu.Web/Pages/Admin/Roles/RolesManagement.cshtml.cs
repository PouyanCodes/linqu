
using Microsoft.AspNetCore.Mvc.RazorPages;
using linqu.Core.DTOs.AdminPanel.Roles;
using linqu.Core.Interfaces;
using System.Collections.Generic;
using linqu.Core.Security;

namespace linqu.Web.Pages.Admin.Roles
{
    [PermissionChecker(5)]
    public class RolesManagement : PageModel
    {
        private readonly IRoleService _roleService;

        public RolesManagement(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public List<RoleDataViewModel> RolesList { get; set; }

        public void OnGet(int id)
        {
            RolesList = _roleService.GetRoles();
        }
    }
}
