
using linqu.Core.Convertors;
using linqu.Core.DTOs.Account;
using linqu.Core.Generators;
using linqu.Core.Interfaces;
using linqu.Core.Security;
using linqu.Core.Senders;
using linqu.DataLayer.Entities.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace linqu.Web.Pages.Shared
{
    public class RegisterModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IViewRenderService _viewRender;


        public RegisterModel(IUserService userService, IRoleService roleService, IViewRenderService viewRender)
        {
            _userService = userService;
            _roleService = roleService;
            _viewRender = viewRender;
        }


        [BindProperty]
        public RegisterViewModel Register { get; set; }


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

            if (_userService.IsEmailExist(FixText.FixEmail(Register.Email)))
            {
                ModelState.AddModelError("Register.Email", "این ایمیل قبلا در سایت استفاده شده است");
                return Page();
            }

            UserInformation userInfo = new UserInformation()
            {
                FullName = Register.FullName
            };

            linqu.DataLayer.Entities.Users.User user = new User()
            {
                Email = Register.Email,
                Password = HashEncode.GetHashedString(Register.Password),
                UserInformation = userInfo,
                UserAvatar = "Default.png",
                RegisterDate = DateTime.Now,
                ConfirmationCode = CodeGenerator.GenerateUniqCode(),
                IsActive = false
            };

            int userId = _userService.AddUser(user);
            _roleService.AddRoleToUser(userId, 3);  // Add Normal Role To User

            #region Send Activation Email

            AccountActivationViewModel accountActivationModel = new AccountActivationViewModel()
            {
                FullName = Register.FullName,
                Email = Register.Email,
                ConfirmationCode = user.ConfirmationCode
            };

            string emailBody = _viewRender.RenderToStringAsync("_AccountActivationEmail", accountActivationModel);
            EmailSender.Send(user.Email, "فعال سازی حساب کاربری", emailBody, true);

            #endregion

            return RedirectToPage("SuccessRegister");
        }
    }
}
