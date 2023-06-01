
using linqu.Core.DTOs.Link;
using linqu.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using linqu.Core.Generators;
using linqu.Core.Security;
using AyrinMovie.Core.Security;
using Microsoft.AspNetCore.Authorization;

namespace linqu.Web.Pages.UserPanel.Links
{
    [Authorize]
    public class CreateLinkModel : PageModel
    {
        private readonly ILinkService _linkService;


        public CreateLinkModel(ILinkService linkService)
        {
            _linkService = linkService;
        }


        [BindProperty]
        public CreateLinkViewModel NewLink { get; set; }


        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            if (!Validation.URLValidation(NewLink.LinkAddress))
            {
                ModelState.AddModelError("NewLink.LinkAddress", "آدرس اینترنتی شما معتبر نیست");
                return Page();
            }

            if (string.IsNullOrWhiteSpace(NewLink.LinkTitle))
                NewLink.LinkTitle = "بدون عنوان";

            NewLink.LinkOwnerId = User.Identity.GetUserId();

            NewLink.ShortKey = CodeGenerator.GenerateUniqCode(5);

            while (_linkService.IsLinkExist(NewLink.ShortKey))
                NewLink.ShortKey = CodeGenerator.GenerateUniqCode(5);

            _linkService.AddLink(NewLink);
            return RedirectToPage("LinksManagement");
        }
    }
}
