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
using Project_Ads.MVVM.Model;

namespace Project_Ads
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string PATH = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Properties";
        private static User user;
        public static NpgsqlConnection Conn = new NpgsqlConnection(Connection.ConnString);

        public static void Authorize(string login, string pass)
        {
            user = Session.GetUser(login, pass);
        }

        public static void CreateAdvertisements(
            User user, Advertisement.Type advType, string address, string description,
            DateTime dateEvent, Animal.Types anType, Animal.Colors animalColor, string pic)
        {
            AdvertisementCollection.CreateAdvertisements(user, advType, address, description, dateEvent, anType, animalColor, pic);
        }

        public static List<Advertisement> GetAdvertisementList()
        {
            var advertisements = AdvertisementCollection.GetAdvertisementList();
            return advertisements;
        }

        public static List<Advertisement> GetUserAdvertisementList(User user)
        {
            var advertisements = AdvertisementCollection.GetUserAdvertisementList(user);
            return advertisements;
        }

        public static Advertisement OpenAdvertisement(int regNum)
        {
            return AdvertisementCollection.OpenAdvertisement(regNum);
        }

        public static void EditAdvertisement(
            int regNum, string address, string description, DateTime dateEvent,
            Animal.Colors animalColor, string pic)
        {
            AdvertisementCollection.EditAdvertisement(regNum, address, description, dateEvent, animalColor, pic);
        }

        public static void DeleteAdvertisement(int regNum)
        {
            AdvertisementCollection.DeleteAdvertisement(regNum);
        }
    }
}
