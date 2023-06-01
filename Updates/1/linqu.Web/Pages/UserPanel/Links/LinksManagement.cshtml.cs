
using linqu.Core.DTOs.Link;
using linqu.Core.Interfaces;
using linqu.Core.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace linqu.Web.Pages.UserPanel
{
    public class LinksManagementModel : PageModel
    {
        private readonly ILinkService _linkService;


        public LinksManagementModel(ILinkService linkService)
        {
            _linkService = linkService;
        }


        [BindProperty]
        public List<LinkDataViewModel> Links { get; set; }


        public void OnGet()
        {
            Links = _linkService.GetUserLinks(User.Identity.GetUserId());
        }
    }
}
