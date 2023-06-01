
using linqu.Core.DTOs.UserPanel;
using linqu.Core.Interfaces;
using linqu.Core.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace linqu.Web.Pages.UserPanel
{
    public class DashboardModel : PageModel
    {
        private readonly IUserService _userService;


        public DashboardModel(IUserService userService)
        {
            _userService = userService;
        }


        [BindProperty]
        public UserInformationViewModel UserInformation { get; set; }


        public IActionResult OnGet()
        {
            UserInformation = _userService.GetUserInformaionById(User.Identity.GetUserId());
            return Page();
        }
    }
}
