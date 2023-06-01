using linqu.Core.Interfaces;
using linqu.Core.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace linqu.Web.Pages.UserPanel.Links
{
    [Authorize]
    public class DeleteLinkModel : PageModel
    {
        private readonly ILinkService _linkService;

        public DeleteLinkModel(ILinkService linkService)
        {
            _linkService = linkService;
        }


        public IActionResult OnGet(int id)
        {
            if (!_linkService.IsLinkExist(id))
                return NotFound();

            if (!_linkService.CheckLinkOwner(User.Identity.GetUserId(), id))
                return NotFound();

            _linkService.DeleteLink(id);
            return RedirectToPage("LinksManagement");
        }
    }
}
