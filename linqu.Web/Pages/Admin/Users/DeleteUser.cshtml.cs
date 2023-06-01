
using linqu.Core.Interfaces;
using linqu.Core.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace linqu.Web.Pages.Admin.Users
{
    [PermissionChecker(4)]
    public class DeleteUserModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;


        public DeleteUserModel(IUserService userService , IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }


        public IActionResult OnGet(int id)
        {
            if (!_userService.IsUserExist(id))
                return NotFound();

            #region Prevent User Editing With Higher Or Equal Role

            var myRoleMagnitute = _roleService.GetRoleMagnitudeOrder(User.Identity.GetUserRoleId());
            var userRole = _userService.GetUserRoleById(id);

            if (myRoleMagnitute >= _roleService.GetRoleMagnitudeOrder(userRole))
                return NotFound();


            #endregion

            _userService.DeleteUser(id);

            // Delete User Avatar

            var userAvatar = _userService.GetUserAvatarById(id);
            _userService.DeleteUserAvatar(userAvatar);

            return RedirectToPage("UsersManagement");

        }
    }
}
