using linqu.Core.DTOs.Account;
using linqu.Core.Generators;
using linqu.Core.Interfaces;
using linqu.Core.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace linqu.Web.Pages.Account
{
    public class ResetPasswordModel : PageModel
    {
        private readonly IUserService _userService;


        public ResetPasswordModel(IUserService userService)
        {
            _userService = userService;
        }


        [BindProperty]
        public ResetPasswordViewModel Reset { get; set; }


        public IActionResult OnGet(string confirmationCode)
        {
            if (_userService.GetUserByConfirmationCode(confirmationCode) == null)
                return NotFound();
            else
            {
                Reset = new ResetPasswordViewModel();
                Reset.ConfirmationCode = confirmationCode;

                return Page();
            }
        }


        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            var user = _userService.GetUserByConfirmationCode(Reset.ConfirmationCode);

            if (user == null)
                return NotFound();

            user.Password = HashEncode.GetHashedString(Reset.Password);
            user.ConfirmationCode = CodeGenerator.GenerateUniqCode();
            _userService.UpdateUser(user);

            return RedirectToPage("Login");
        }
    }
}
