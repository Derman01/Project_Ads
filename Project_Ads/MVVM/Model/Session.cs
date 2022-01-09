using Project_Ads.Core;
using Project_Ads.Model;

namespace Project_Ads.MVVM.Model
{
    public class Session
    {
        public static User GetUser(string login, string pass)
        {
            //Connection.Execute("SELECT * FROM User u WHERE u.login = @login AND u.password = @pass");
            var user = new User();
            return user;
        }

        public static void Authorize(string login, string password)
        {
            
        }

        public static void Registrate(string login, string password, string userName, string phone)
        {
            
        }
    }
}