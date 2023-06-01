
using linqu.Core.DTOs.Account;
using linqu.Core.DTOs.AdminPanel;
using linqu.Core.DTOs.UserPanel;
using linqu.DataLayer.Entities.Users;
using System.Collections.Generic;

namespace linqu.Core.Interfaces
{
    public interface IUserService
    {
        int AddUser(User user);
        int AddUser(CreateUserViewModel user);
        void EditUser(EditUserViewModel editedUser);
        void EditProfile(EditProfileViewModel edit);
        void ChangeUserPassword(string email, string newPassword);
        void UpdateUser(User user);
        void DeleteUser(int userId);
        void DeleteUserAvatar(string image);
        User LoginUser(LoginViewModel login);
        bool ActiveAccount(string confirmationCode);



        List<UserDataViewModel> GetUsers();
        User GetUserById(int userId);
        User GetUserByEmail(string email, bool isActive = true);
        User GetUserByConfirmationCode(string Code);
        UserInformationViewModel GetUserInformaionById(int userId);
        UserInformationViewModel GetUserInformaionByEmail(string email, bool isActive = true);
        EditUserViewModel GetUserDataForEdit(int userId);
        EditProfileViewModel GetUserDataForEditProfile(int userId);
        EditProfileViewModel GetUserDataForEditProfile(string email, bool isActive = true);
        int GetUserIdByEmail(string email, bool isActive = true);
        string GetUserAvatarById(int id);
        string GetUserAvatarByEmail(string email, bool isActive = true);
        string GetFullNameById(int id);
        string GetFullNameByEmail(string email, bool isActive = true);
        int GetUserRoleById(int id);
        int GetUserRoleByEmail(string email, bool isActive = true);



        bool IsEmailExist(string email, bool isActive = true);
        bool IsUserExist(int userId);
        bool CompareOldPassword(string email, string oldPassword, bool isActive = true);
        bool MatchEmailWithId(int userId, string email);
        bool IsGreatAdmin(int userId);
    }
}
