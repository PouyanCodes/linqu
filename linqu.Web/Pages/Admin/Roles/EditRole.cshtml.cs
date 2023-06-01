
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using linqu.Core.DTOs.AdminPanel;
using linqu.Core.Interfaces;
using System.Collections.Generic;
using linqu.Core.Security;

namespace linqu.Web.Pages.Admin.Roles
{

    [PermissionChecker(7)]

    public class EditRoleModel : PageModel
    {
        private readonly IRoleService _roleService;
        private readonly IPermissionService _permissionService;

        public EditRoleModel(IPermissionService permissionService, IRoleService roleService)
        {
            _roleService = roleService;
            _permissionService = permissionService;
        }

        [BindProperty]
        public EditRoleViewModel EditedRole { get; set; }

        public IActionResult OnGet(int id)
        {
            if (_roleService.IsGreatAdmin(id))
                return NotFound();

            EditedRole = _roleService.GetRoleDataForEdit(id);
            return Page();
        }

        public IActionResult OnPost(List<int> selectedPermission)
        {
            if (!ModelState.IsValid)
                return Page();


            if (_roleService.IsRoleExist(EditedRole.RoleTitle) &&
                !_roleService.MatchRoleTitleWithId(EditedRole.RoleId, EditedRole.RoleTitle))
            {
                ModelState.AddModelError("EditedRole.RoleTitle", "این نقش هم اکنون در سایت موجود است");
                return Page();
            }


            if (EditedRole.OrderOfMagnitude == 0)
            {
                ModelState.AddModelError("EditedRole.OrderOfMagnitude", "امکان ساخت کاربر با مرتبه بزرگی صفر وجود ندارد");
                return Page();
            }

            _roleService.EditRole(EditedRole);

            _permissionService.EditRolePermissions(EditedRole.RoleId, selectedPermission);

            return RedirectToPage("RolesManagement");
        }
    }
}
