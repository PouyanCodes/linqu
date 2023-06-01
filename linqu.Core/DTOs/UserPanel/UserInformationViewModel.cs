
using System;
using System.ComponentModel.DataAnnotations;

namespace linqu.Core.DTOs.UserPanel
{
    public class UserInformationViewModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}
