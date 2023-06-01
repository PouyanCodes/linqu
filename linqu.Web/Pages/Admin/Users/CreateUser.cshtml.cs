


using linqu.Core.DTOs.AdminPanel;
using linqu.Core.Interfaces;
using linqu.Core.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace linqu.Web.Pages.Admin.Users
{
    [PermissionChecker(2)]
    public class CreateUserModel : PageModel
    {

        private readonly IUserService _userService;
        private readonly IRoleService _roleService;


        public CreateUserModel(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }


        [BindProperty]
        public CreateUserViewModel NewUser { get; set; }


        public IActionResult OnPost(List<int> selectedRoles)
        {
            if (!ModelState.IsValid)
                return Page();

            #region Checking The Existence Of Email


            if (_userService.IsEmailExist(NewUser.Email))
            {
                ModelState.AddModelError("NewUser.Email",
                    "این ایمیل هم اکنون در سایت موجود می باشد ؛ ایمیل دیگری را امتحان کنید ");
                return Page();
            }


            #endregion

            #region Prevent User Creation With Higher Or Equal Role


            var myRoleMagnitude = _roleService.GetRoleMagnitudeOrder(User.Identity.GetUserRoleId());

            if (myRoleMagnitude >= _roleService.GetRoleMagnitudeOrder(selectedRoles[0]))
            {
                ModelState.AddModelError("NewUser.ConfirmPassword",
                    "شما دسترسی لازم برای ساخت کاربری با این نقش را ندارید ، لطقا نقش کاربر را عوض کنید");
                return Page();
            }


            #endregion


            // Encode Password
            NewUser.Password = HashEncode.GetHashedString(NewUser.Password);

            int userId = _userService.AddUser(NewUser);

            // Add Roles 
            _roleService.AddRolesToUser(userId, selectedRoles);

            return RedirectToPage("UsersManagement");
        }
    }
}
