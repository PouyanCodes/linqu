
using linqu.Core.DTOs.AdminPanel;
using linqu.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using linqu.Core.Security;

namespace linqu.Web.Pages.Admin.Roles
{

    [PermissionChecker(6)]   

    public class CreateRoleModel : PageModel
    {
        private readonly IRoleService _roleService;
        private readonly IPermissionService _permissionService;


        public CreateRoleModel(IRoleService roleService, IPermissionService permissionService)
        {
            _roleService = roleService;
            _permissionService = permissionService;
        }


        [BindProperty]
        public CreateRoleViewModel NewRole { get; set; }


        public IActionResult OnPost(List<int> selectedPermission)
        {
            if (!ModelState.IsValid)
                return Page();


            if (_roleService.IsRoleExist(NewRole.RoleTitle))
            {
                ModelState.AddModelError("NewRole.RoleTitle", "این نقش هم اکنون در سایت موجود است");
                return Page();
            }

            if (NewRole.OrderOfMagnitude == 0)
            {
                ModelState.AddModelError("NewRole.OrderOfMagnitude", "امکان ساخت کاربر با مرتبه بزرگی صفر وجود ندارد");
                return Page();
            }

            if (selectedPermission.Count == 0)
            {
                ModelState.AddModelError("NewRole.RoleTitle", "لطفا دسترسی هایی را برای نقش انتخاب کنید");
                return Page();
            }

            int roleId = _roleService.AddRole(NewRole);

            _permissionService.AddPermissionsToRole(roleId, selectedPermission);

            return RedirectToPage("RolesManagement");
        }
    }
}
