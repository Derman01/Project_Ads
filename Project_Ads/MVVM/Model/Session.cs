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
                UserRights = new Rights(false, false, false, true, false),
                UserRole = User.Role.NotAuthorizedUser
            };
        }

        public static void Authorize(string login, string password)
        {
            currentUser = Connection.ExecuteUserAuthorization(
                "SELECT u.id, u.name, u.phone, r.role_name FROM \"user\" u INNER JOIN role r on r.id = u.id_role WHERE u.login = @login AND u.password = @password",
                login, password);
        }

        public static void Registrate(string login, string password, string userName, string phone)
        {
            var userId = Connection.ExecuteUserRegistration(
                "INSERT INTO \"user\" (name, login, password, phone, id_role) VALUES (@name, @login, @password, @phone, @role) RETURNING id",
                userName, login, password, phone);
            currentUser = new User
            {
                UserId = userId,
                UserName = userName,
                UserPhone = phone,
                UserRights = new Rights(true, true, true, true, false),
                UserRole = User.Role.User,
            };
        }
    }
}