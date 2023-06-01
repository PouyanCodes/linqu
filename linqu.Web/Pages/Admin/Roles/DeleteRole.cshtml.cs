
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using linqu.Core.Interfaces;
using System.Collections.Generic;
using linqu.Core.Security;

namespace linqu.Web.Pages.Admin.Roles
{

    [PermissionChecker(8)]

    public class DeleteRoleModel : PageModel
    {

        private readonly IRoleService _roleService;

        public DeleteRoleModel(IRoleService roleService)
        {
            _roleService = roleService;
        }


        [BindProperty]
        public int roleId { get; set; }


        public void OnGet(int id)
        {
            roleId = id;
        }


        public IActionResult OnPost(List<int> selectedRoles)
        {
            var userIds = _roleService.GetUsersByRole(roleId);
            _roleService.EditMultipleUsersRole(userIds, roleId, selectedRoles[0]);
            _roleService.DeleteRole(roleId);
            return RedirectToPage("RolesManagement");
        }
    }
}
