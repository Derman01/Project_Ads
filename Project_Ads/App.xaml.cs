using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Npgsql;
using Project_Ads.Core;
using Project_Ads.Model;

namespace Project_Ads
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string PATH = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Properties";
        private static User _user;
        public static NpgsqlConnection Conn = new NpgsqlConnection(Connection.ConnString);

        public static User User
        {
            get => _user;
            set => _user = value;
        }

        public App()
        { // пример создания юзера, наверное
            var defaulRights = new Rights(false, false, false, false, false);
            User = new User() {UserRights = defaulRights, UserName = "Гость", UserRole = User.Role.NotAuthorizedUser };
        }
        
    }
}
