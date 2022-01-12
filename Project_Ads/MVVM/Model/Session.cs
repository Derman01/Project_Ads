using System;
using Project_Ads.Core;

namespace Project_Ads.MVVM.Model
{
    public static class Session
    {
        private static User currentUser { get; set; }
        
        public static User GetUser()
        {
            return currentUser;
        }

        public static void CreateGuest()
        {
            currentUser = new User()
            {
                UserId = 0,
                UserName = "Гость",
                UserRole = User.Role.NotAuthorizedUser
            };
        }

        public static void Authorize(string login, string password)
        {
            currentUser = Connection.ExecuteUserAuthorization(login, password);
        }

        public static void Registrate(string login, string password, string userName, string phone)
        {
            var userId = Connection.ExecuteUserRegistration(userName, login, password, phone);
            currentUser = new User
            {
                UserId = userId,
                UserName = userName,
                UserPhone = phone,
                UserRole = User.Role.User,
            };
        }
    }
}