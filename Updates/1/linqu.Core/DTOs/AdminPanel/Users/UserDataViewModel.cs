
using System;


namespace linqu.Core.DTOs.UserPanel
{
    public class UserDataViewModel
    {
        public int userId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserAvatar { get; set; }
        public DateTime RegisterDate { get; set; }
        public string UserRole { get; set; }
        public int UserMagnitude { get; set; }
    }
}
