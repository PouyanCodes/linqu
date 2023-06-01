

using linqu.Core.DTOs.UserPanel;
using System.Collections.Generic;

namespace linqu.Core.DTOs.AdminPanel
{
    public class UsersListFilterViewModel
    {
        public List<UserDataViewModel> Users { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
}
