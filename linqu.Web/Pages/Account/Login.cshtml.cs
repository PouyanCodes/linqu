
using linqu.Core.DTOs.Account;
using linqu.Core.Interfaces;
using linqu.Core.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Security.Claims;

namespace linqu.Web.Pages.Shared
{
    public class LoginModel : PageModel
    {
        private readonly IUserService _userService;


        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }


        [BindProperty]
        public LoginViewModel Login { get; set; }


        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToPage("/Userpanel/Dashboard");
            else
                return Page();
        }


        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            Login.Password = HashEncode.GetHashedString(Login.Password);
            var user = _userService.LoginUser(Login);

            if (user != null)
            {
                if (user.IsActive)
                {
                    #region Claim Authentication

                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),

                        new Claim(ClaimTypes.Name,user.Email),  // Using Email Instead Of Username

                        new Claim(ClaimTypes.Role,_userService.GetUserRoleByEmail(user.Email).ToString())
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    var properties = new AuthenticationProperties
                    {
                        IsPersistent = Login.RememberMe
                    };

                    HttpContext.SignInAsync(principal, properties);

                    #endregion

                    return RedirectToPage("/Userpanel/Dashboard");
                }
                else
                {
                    ModelState.AddModelError("Login.Email", "حساب کاربری شما فعال نمی باشد");
                    return Page();
                }
            }

            ModelState.AddModelError("Login.Email", "کاربری با مشخصات وارد شده یافت نشد");
            return Page();
        }
    }
}
