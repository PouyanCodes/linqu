
using linqu.Core.DTOs.AdminPanel;
using linqu.Core.Interfaces;
using linqu.Core.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace linqu.Web.Pages.Admin.Users
{
    [PermissionChecker(3)]
    public class EditUserModel : PageModel
    {

        private readonly IUserService _userService;
        private readonly IRoleService _roleService;


        public EditUserModel(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }


        [BindProperty]
        public EditUserViewModel EditedUser { get; set; }


        public IActionResult OnGet(int id)
        {
            // Checking The Existence Of User 

            if (!_userService.IsUserExist(id))
                return NotFound();

            #region Prevent User Editing With Higher Or Equal Role

            var myRoleMagnitude = _roleService.GetRoleMagnitudeOrder(User.Identity.GetUserRoleId());
            var userRole = _userService.GetUserRoleById(id);

            if (myRoleMagnitude >= _roleService.GetRoleMagnitudeOrder(userRole))
                return NotFound();

            #endregion

            EditedUser = _userService.GetUserDataForEdit(id);
            return Page();
        }


        public IActionResult OnPost(List<int> selectedRoles)
        {
            if (!ModelState.IsValid)
                return Page();

            #region Checking The Existence Of Email


            if (_userService.IsEmailExist(EditedUser.Email) &&
                !_userService.MatchEmailWithId(EditedUser.UserId, EditedUser.Email))
            {
                ModelState.AddModelError("EditedUser.Email", "این ایمیل هم اکنون در سایت موجود می باشد ؛ ایمیل دیگری را امتحان کنید ");
                return Page();
            }


            #endregion


            // Encode Password

            if (!string.IsNullOrWhiteSpace(EditedUser.Password))
                EditedUser.Password = HashEncode.GetHashedString(EditedUser.Password);

            _userService.EditUser(EditedUser);

            // Edit Roles
            _roleService.EditUserRoles(EditedUser.UserId, selectedRoles);

            return RedirectToPage("UsersManagement");
        }
    }
}
