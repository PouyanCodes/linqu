using linqu.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace linqu.Web.Pages.Account
{
    public class ActiveAccountModel : PageModel
    {
        private readonly IUserService _userService;


        public ActiveAccountModel(IUserService userService)
        {
            _userService = userService;
        }


        public IActionResult OnGet(string confirmationCode)
        {
            if (string.IsNullOrWhiteSpace(confirmationCode))
                return NotFound();

            var isSuccess = _userService.ActiveAccount(confirmationCode);

            if (isSuccess)
                return Page();
            else
                return NotFound();
        }
    }
}
