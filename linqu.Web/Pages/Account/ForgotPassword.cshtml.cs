
using linqu.Core.Convertors;
using linqu.Core.DTOs.Account;
using linqu.Core.Interfaces;
using linqu.Core.Senders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace linqu.Web.Pages
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IViewRenderService _viewRenderService;


        public ForgotPasswordModel(IUserService userService, IViewRenderService viewRenderService)
        {
            _userService = userService;
            _viewRenderService = viewRenderService;
        }


        [BindProperty]
        public ForgotPasswordViewModel Forgot { get; set; }


        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToPage("/userpanel");
            else
                return Page();
        }


        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            string fixedEmail = FixText.FixEmail(Forgot.Email);
            DataLayer.Entities.Users.User user = _userService.GetUserByEmail(fixedEmail);

            if (user == null)
            {
                ModelState.AddModelError("Forgot.Email", "کاربری یافت نشد");
                return Page();
            }

            if (!user.IsActive)
            {
                ModelState.AddModelError("Forgot.Email", "این حساب هنوز فعال سازی نشده است");
                return Page();
            }

            string emailBody = _viewRenderService.RenderToStringAsync("_ForgotPasswordEmail", user);
            EmailSender.Send(user.Email, "بازیابی حساب کاربری", emailBody, true);

            ModelState.AddModelError("Forgot.Email", "ایمیل فعال سازی برای شما ارسال شد");   // Fix Later

            return Page();
        }
    }
}
