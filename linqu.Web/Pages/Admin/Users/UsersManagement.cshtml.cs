
using linqu.Core.DTOs.UserPanel;
using linqu.Core.Interfaces;
using linqu.Core.Security;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;


namespace linqu.Web.Pages.Admin.Users
{
    [PermissionChecker(1)]
    public class UsersManagement : PageModel
    {
        private readonly IUserService _userService;

        public UsersManagement(IUserService userService)
        {
            _userService = userService;
        }

        public List<UserDataViewModel> Users { get; set; }

        public void OnGet()
        {
            Users = _userService.GetUsers();
        }
    }
}
