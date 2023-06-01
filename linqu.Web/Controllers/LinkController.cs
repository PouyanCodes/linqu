using linqu.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace linqu.Web.Controllers
{
    public class LinkController : Controller
    {
        private ILinkService _linkService;


        public LinkController(ILinkService linkService)
        {
            _linkService = linkService;
        }


        [Route("l/{id}")]
        public IActionResult GoLinkAddress(string id)
        {
            if (!_linkService.IsLinkExist(id))
                return NotFound();

            var address = _linkService.GetLinkWebAddress(id);

            return Redirect(address);
        }
    }
}
