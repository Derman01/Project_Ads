using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Npgsql;
using Project_Ads.Core;
using Project_Ads.MVVM.Model;

namespace Project_Ads
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string PATH = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\MVVM\View\Icons\";
        public static NpgsqlConnection Conn = new NpgsqlConnection(Connection.ConnString);

        public static void CreateAdvertisements( Advertisement.AdvertisementType advAdvertisementType, string address, string description,
            DateTime dateEvent, Animal.Types anType, string animalColor, string pic)
        {
            AdvertisementCollection.CreateAdvertisements(Session.currentUser, advAdvertisementType, address, description, dateEvent, anType, animalColor, pic);
        }

        public static ObservableCollection<Advertisement> GetAdvertisementList 
            => AdvertisementCollection.GetAdvertisementList;
        public static ObservableCollection<Advertisement> GetAdvertisementListByUser
            => AdvertisementCollection.GetAdvertisementsByUser;

        public static Advertisement GetAdvertisement(int regNum)
            => AdvertisementCollection.GetAdvertisement(regNum);
        
        public static void EditAdvertisement(
            int regNum, string address, string description, DateTime dateEvent,
            string animalColor, string pic)
        {
            AdvertisementCollection.EditAdvertisement(regNum, address, description, dateEvent, animalColor, pic);
        }

        public static void DeleteAdvertisement(int regNum)
        {
            AdvertisementCollection.DeleteAdvertisement(regNum);
        }

        public static void Authorization(string login, string password)
        {
            Session.Authorize(login, password);
        }

        public static void Registration(string login, string password, string userName, string phone)
        {
            Session.Registrate(login, password, userName, phone);
        }

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
        }
    }
}
