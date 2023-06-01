using linqu.Core.DTOs.UserPanel;
using linqu.Core.Interfaces;
using linqu.Core.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace linqu.Web.Pages.UserPanel
{
    public class EditProfileModel : PageModel
    {
        private readonly IUserService _userService;


        public EditProfileModel(IUserService userService)
        {
            _userService = userService;
        }


        [BindProperty]
        public EditProfileViewModel Edit { get; set; }


        public IActionResult OnGet()
        {
            Edit = _userService.GetUserDataForEditProfile(User.Identity.GetUserId());
            return Page();
        }


        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            _userService.EditProfile(Edit);
            return RedirectToPage("Dashboard");
        }
    }
}
