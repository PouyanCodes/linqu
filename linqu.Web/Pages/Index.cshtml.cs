using AyrinMovie.Core.Security;
using linqu.Core.DTOs.Link;
using linqu.Core.Generators;
using linqu.Core.Interfaces;
using linqu.Core.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace linqu.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ILinkService _linkService;


        public IndexModel(ILogger<IndexModel> logger, ILinkService linkService)
        {
            _logger = logger;
            _linkService = linkService;
        }


        [BindProperty]
        public CreateLinkViewModel NewLink { get; set; }


        public IActionResult OnPost()
        {
            if (!User.Identity.IsAuthenticated)
            {
                ModelState.AddModelError("NewLink.LinkAddress", "لطفا ابتدا وارد حساب خود شوید");
                return Page();
            }

            if (!ModelState.IsValid)
                return Page();

            if (!Validation.URLValidation(NewLink.LinkAddress))
            {
                ModelState.AddModelError("NewLink.LinkAddress", "آدرس اینترنتی شما معتبر نیست");
                return Page();
            }

            NewLink.LinkTitle = "بدون عنوان";
            NewLink.LinkOwnerId = User.Identity.GetUserId();

            NewLink.ShortKey = CodeGenerator.GenerateUniqCode(5);

            while (_linkService.IsLinkExist(NewLink.ShortKey))
                NewLink.ShortKey = CodeGenerator.GenerateUniqCode(5);

            _linkService.AddLink(NewLink);
            ModelState.AddModelError("NewLink.LinkAddress",
                "  لینک شما با موفقیت ایجاد شد  : " + "https://linqu/l/" + NewLink.ShortKey);

            return Page();
        }


    }
}
