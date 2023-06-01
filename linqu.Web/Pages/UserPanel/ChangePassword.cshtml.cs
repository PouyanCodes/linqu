
using linqu.Core.DTOs.UserPanel;
using linqu.Core.Interfaces;
using linqu.Core.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace linqu.Web.Pages.UserPanel
{
    [Authorize]
    public class ChangePasswordModel : PageModel
    {
        private readonly IUserService _userService;


        public ChangePasswordModel(IUserService userService)
        {
            _userService = userService;
        }


        [BindProperty]
        public ChangePasswordViewModel Change { get; set; }


        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();


            if (!_userService.CompareOldPassword(User.Identity.Name,
                HashEncode.GetHashedString(Change.OldPassword)))
            {
                ModelState.AddModelError("Change.OldPassword", "کلمه عبور فعلی صحیح نمیباشد");
                return Page();
            }

            _userService.ChangeUserPassword(User.Identity.Name,
                HashEncode.GetHashedString(Change.Password));

            return Page();
        }
    }
}
