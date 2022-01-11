using System;
using Project_Ads.Core;
using Project_Ads.Model;

namespace Project_Ads.MVVM.Model
{
    public class Session
    {
        private static User currentUser { get; set; }
        
        public static User GetUser()
        {
            return currentUser;
        }

        public static void Authorize(string login, string password)
        {
            var data = new Tuple<string, object>[]
            {
                new Tuple<string, object>("login", login),
                new Tuple<string, object>("password", password)
            };

            var user = Connection.ExecuteUserAuthorization(
                "SELECT u.id, u.name, u.phone, r.role_name FROM \"user\" u INNER JOIN role r on r.id = u.id_role WHERE u.login = @login AND u.password = @password",
                data);
        }

        public static void Registrate(string login, string password, string userName, string phone)
        {
            var data = new Tuple<string, object>[]
            {
                new Tuple<string, object>("name", userName),
                new Tuple<string, object>("login", login),
                new Tuple<string, object>("password", password),
                new Tuple<string, object>("phone", phone),
                new Tuple<string, object>("role", 3) // 3 - default user
            };
            var userId = Connection.ExecuteUserRegistration(
                "INSERT INTO \"user\" VALUES (name = @name, login = @login, password = @password, phone = @phone, id_role = @role) RETURNING id",
                data);
            if (userId == -1)
                throw new ArgumentException("Такой логин уже существует");
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