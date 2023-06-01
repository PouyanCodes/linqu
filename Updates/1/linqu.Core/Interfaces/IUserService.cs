
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
        User GetUserByEmail(string email);
        User GetUserByConfirmationCode(string Code);
        UserInformationViewModel GetUserInformaionById(int userId);
        UserInformationViewModel GetUserInformaionByEmail(string email);
        EditUserViewModel GetUserDataForEdit(int userId);
        EditProfileViewModel GetUserDataForEditProfile(int userId);
        EditProfileViewModel GetUserDataForEditProfile(string email);
        int GetUserIdByEmail(string email);
        string GetUserAvatarById(int id);
        string GetUserAvatarByEmail(string email);
        string GetFullNameById(int id);
        string GetFullNameByEmail(string email);
        int GetUserRoleById(int id);
        int GetUserRoleByEmail(string email);



        bool IsEmailExist(string email);
        bool IsUserExist(int userId);
        bool CompareOldPassword(string email, string oldPassword);
        bool MatchEmailWithId(int userId, string email);
        bool IsGreatAdmin(int userId);
    }
}
