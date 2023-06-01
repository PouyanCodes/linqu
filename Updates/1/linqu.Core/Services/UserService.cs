
using linqu.Core.Convertors;
using linqu.Core.DTOs.Account;
using linqu.Core.DTOs.AdminPanel;
using linqu.Core.DTOs.UserPanel;
using linqu.Core.Generators;
using linqu.Core.Interfaces;
using linqu.DataLayer.Context;
using linqu.DataLayer.Entities.Users;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace linqu.Core.Services
{
    public class UserService : IUserService
    {

        private readonly DataBaseContext _context;

        public UserService(DataBaseContext context)
        {
            _context = context;
        }

        public int AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.UserId;
        }

        public int AddUser(CreateUserViewModel user)
        {
            UserInformation userInfo = new UserInformation()
            {
                FullName = user.FullName
            };

            User newUser = new User()
            {
                Password = user.Password,
                ConfirmationCode = CodeGenerator.GenerateUniqCode(),
                Email = user.Email,
                IsActive = true,
                RegisterDate = DateTime.Now,
                UserInformation = userInfo
            };

            #region Save Avatar

            if (user.UserAvatarFile != null)
            {
                // Main Image

                newUser.UserAvatar = CodeGenerator.GenerateUniqCode() + Path.GetExtension(user.UserAvatarFile.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/UserAvatar/", newUser.UserAvatar);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    user.UserAvatarFile.CopyTo(stream);
                }

                // Create Thumbnail

                string thumbnailPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/UserAvatar/Thumbnail/", newUser.UserAvatar);
                Convertors.ImageConvertor.Image_Resize(imagePath, thumbnailPath, 150);

            }

            else
                newUser.UserAvatar = "Default.png";

            #endregion

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return newUser.UserId;
        }

        public void EditUser(EditUserViewModel editedUser)
        {
            User user = _context.Users.Find(editedUser.UserId);
            UserInformation userInfo = _context.UserInformation.Find(editedUser.UserId);


            if (!string.IsNullOrWhiteSpace(editedUser.Password))
                user.Password = editedUser.Password;


            if (editedUser.UserAvatarFile != null)
            {
                //Delete old Image
                DeleteUserAvatar(editedUser.UserAvatar);

                #region Save New Avatar

                // Edit Main Image

                user.UserAvatar = CodeGenerator.GenerateUniqCode() + Path.GetExtension(editedUser.UserAvatarFile.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/UserAvatar", user.UserAvatar);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    editedUser.UserAvatarFile.CopyTo(stream);
                }

                // Edit Thumbnail

                string thumbnailPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/UserAvatar/Thumbnail", user.UserAvatar);
                ImageConvertor.Image_Resize(imagePath, thumbnailPath, 150);

                #endregion
            }

            userInfo.FullName = editedUser.FullName;

            user.Email = editedUser.Email;
            user.IsActive = editedUser.IsActive;
            user.UserInformation = userInfo;

            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void EditProfile(EditProfileViewModel edit)
        {
            var user = _context.Users.Find(edit.UserId);
            var userInfo = _context.UserInformation.Find(edit.UserId);

            if (edit.UserAvatarFile != null)
            {
                // Delete Old Avatar
                DeleteUserAvatar(edit.UserAvatar);

                #region Save New Avatar

                // Edit Main Image

                user.UserAvatar = CodeGenerator.GenerateUniqCode() + Path.GetExtension(edit.UserAvatarFile.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/UserAvatar", user.UserAvatar);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    edit.UserAvatarFile.CopyTo(stream);
                }

                // Edit Thumbnail

                string thumbnailPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/UserAvatar/Thumbnail", user.UserAvatar);
                ImageConvertor.Image_Resize(imagePath, thumbnailPath, 150);

                #endregion
            }
            userInfo.FullName = edit.FullName;

            user.UserInformation = userInfo;
            UpdateUser(user);
        }

        public void ChangeUserPassword(string email, string newPassword)
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == email);
            user.Password = newPassword;
            UpdateUser(user);
        }

        public void UpdateUser(User user)
        {
            _context.Update(user);
            _context.SaveChanges();
        }

        public void DeleteUser(int userId)
        {
            User user = GetUserById(userId);
            user.IsDeleted = true;
            UpdateUser(user);
        }

        public void DeleteUserAvatar(string image)
        {
            if (image != "Default.png")
            {
                string mainImageDeletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/UserAvatar", image);
                string thumbnailDeletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/UserAvatar/Thumbnail", image);

                if (File.Exists(mainImageDeletePath))
                    File.Delete(mainImageDeletePath);

                if (File.Exists(thumbnailDeletePath))
                    File.Delete(thumbnailDeletePath);
            }
        }

        public User LoginUser(LoginViewModel login)
        {
            string email = FixText.FixEmail(login.Email);
            return _context.Users.SingleOrDefault(u => u.Email == email && u.Password == login.Password);
        }

        public bool ActiveAccount(string confirmationCode)
        {
            var user = _context.Users.SingleOrDefault(u => u.ConfirmationCode == confirmationCode);

            if (user == null || user.IsActive)
                return false;

            user.IsActive = true;
            user.ConfirmationCode = CodeGenerator.GenerateUniqCode();
            _context.SaveChanges();

            return true;
        }

        public List<UserDataViewModel> GetUsers()
        {
            return _context.Users.Select(u => new UserDataViewModel()
            {
                userId = u.UserId,
                FullName = _context.UserInformation.SingleOrDefault(i => i.UserId == u.UserId).FullName,
                RegisterDate = u.RegisterDate,
                Email = u.Email,
                UserAvatar = u.UserAvatar,

                UserRole = _context.Roles.SingleOrDefault(r =>
                r.RoleId == _context.UserRoles.SingleOrDefault(x => x.UserId == u.UserId).RoleId).RoleTitle,

                UserMagnitude = _context.Roles.SingleOrDefault(r =>
                r.RoleId == _context.UserRoles.SingleOrDefault(x => x.UserId == u.UserId).RoleId).OrderOfMagnitude

            }).ToList();
        }

        public User GetUserById(int userId)
        {
            return _context.Users.Find(userId);
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.SingleOrDefault(u => u.Email == email);
        }

        public User GetUserByConfirmationCode(string Code)
        {
            return _context.Users.SingleOrDefault(u => u.ConfirmationCode == Code);
        }

        public UserInformationViewModel GetUserInformaionById(int userId)
        {
            var user = _context.Users.Find(userId);
            var userInfo = _context.UserInformation.Find(userId);

            return new UserInformationViewModel()
            {
                Email = user.Email,
                RegisterDate = user.RegisterDate,
                FullName = userInfo.FullName,
            };
        }

        public UserInformationViewModel GetUserInformaionByEmail(string email)
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == email);
            var userInfo = _context.UserInformation.Find(user.UserId);

            return new UserInformationViewModel()
            {
                Email = user.Email,
                RegisterDate = user.RegisterDate,
                FullName = userInfo.FullName,
            };
        }

        public EditUserViewModel GetUserDataForEdit(int userId)
        {
            var user = _context.Users.Find(userId);
            var userInfo = _context.UserInformation.Find(userId);

            return new EditUserViewModel()
            {
                UserId = user.UserId,
                FullName = userInfo.FullName,
                UserAvatar = user.UserAvatar,
                Email = user.Email,
                IsActive = user.IsActive
            };
        }

        public EditProfileViewModel GetUserDataForEditProfile(int userId)
        {
            var user = _context.Users.Find(userId);
            var userInfo = _context.UserInformation.Find(userId);

            return new EditProfileViewModel()
            {
                UserId = user.UserId,
                UserAvatar = user.UserAvatar,
                FullName = userInfo.FullName
            };
        }

        public EditProfileViewModel GetUserDataForEditProfile(string email)
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == email);
            var userInfo = _context.UserInformation.Find(user.UserId);

            return new EditProfileViewModel()
            {
                UserId = user.UserId,
                UserAvatar = user.UserAvatar,
                FullName = userInfo.FullName
            };
        }

        public int GetUserIdByEmail(string email)
        {
            return _context.Users.Single(u => u.Email == email).UserId;
        }

        public string GetUserAvatarById(int id)
        {
            return _context.Users.Find(id).UserAvatar;
        }

        public string GetUserAvatarByEmail(string email)
        {
            return _context.Users.SingleOrDefault(u => u.Email == email).UserAvatar;
        }

        public string GetFullNameById(int id)
        {
            return _context.UserInformation.Find(id).FullName;
        }

        public string GetFullNameByEmail(string email)
        {
            var userId = GetUserIdByEmail(email);
            return _context.UserInformation.Find(userId).FullName;
        }

        public int GetUserRoleById(int id)
        {
            return _context.UserRoles.SingleOrDefault(u => u.UserId == id).RoleId;
        }

        public int GetUserRoleByEmail(string email)
        {
            var userId = _context.Users.SingleOrDefault(u => u.Email == email).UserId;
            return _context.UserRoles.SingleOrDefault(u => u.UserId == userId).RoleId;
        }

        public bool IsEmailExist(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        public bool IsUserExist(int userId)
        {
            return _context.Users.Any(u => u.UserId == userId);
        }

        public bool CompareOldPassword(string email, string oldPassword)
        {
            return _context.Users.Any(u => u.Email == email && u.Password == oldPassword);
        }

        public bool MatchEmailWithId(int userId, string email)
        {
            return _context.Users.Any(u => u.UserId == userId && u.Email == email);
        }

        public bool IsGreatAdmin(int userId)
        {
            var role = _context.Roles.Find(_context.UserRoles.Single(u => u.UserId == userId).RoleId);

            if (role.OrderOfMagnitude == 0)
                return true;
            else
                return false;
        }

    }
}
