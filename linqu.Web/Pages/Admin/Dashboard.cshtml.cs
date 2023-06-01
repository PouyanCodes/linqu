
using linqu.Core.Security;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace linqu.Web.Pages.Admin
{
    [PermissionChecker(9)]

    public class DashboardModel : PageModel
    {     
        public void OnGet()
        {
        }
    }
}
