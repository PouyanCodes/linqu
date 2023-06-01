
using linqu.Core.DTOs.Link;
using linqu.Core.Interfaces;
using linqu.Core.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace linqu.Web.Pages.UserPanel.Links
{
    [Authorize]
    public class EditLinkModel : PageModel
    {

        private readonly ILinkService _linkService;


        public EditLinkModel(ILinkService linkService)
        {
            _linkService = linkService;
        }


        [BindProperty]
        public EditLinkViewModel EditedLink { get; set; }


        public IActionResult OnGet(int id)
        {
            if (!_linkService.IsLinkExist(id))
                return NotFound();

            if (!_linkService.CheckLinkOwner(User.Identity.GetUserId(), id))
                return NotFound();

            EditedLink = _linkService.GetLinkDataForEdit(id);
            return Page();
        }


        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            _linkService.EditLink(EditedLink);
            return RedirectToPage("LinksManagement");
        }
    }
}
