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
        public static string PATH = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\";

        public static void CreateAdvertisements( int advertisementType, string address, string description,
            DateTime dateEvent, int anType, string animalColor, string pic)
        {
            var regNum = AdvertisementCollection.CreateAdvertisements(
                Session.GetUser(), advertisementType, address, description, dateEvent, anType, animalColor, pic);
            Logger.Adding(regNum.ToString());
        }

        public static List<Dictionary<string, object>> GetAdvertisementList 
            => AdvertisementCollection.GetAdvertisementList;
        
        public static List<Dictionary<string, object>> GetAdvertisementListByUser
            => AdvertisementCollection.GetAdvertisementsByUser(Session.GetUser());

        public static void EditAdvertisement(
            int regNum, string address, string description, DateTime dateEvent,
            string animalColor, string pic)
        {
            AdvertisementCollection.EditAdvertisement(regNum, address, description, dateEvent, animalColor, pic);
            Logger.Eding(regNum.ToString());
        }

        public static void DeleteAdvertisement(int regNum)
        {
            AdvertisementCollection.DeleteAdvertisement(regNum);
            Logger.Deleting(regNum.ToString());
        }

        public static void Authorization(string login, string password)
        {
            Session.Authorize(login, password);
        }

        public static void Registration(string login, string password, string userName, string phone)
        {
            Session.Registrate(login, password, userName, phone);
        }

        public static string GetUserName() => Session.GetUser().UserName;
        
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            Logger.InitState();
            var advs = GetAdvertisementList;
            Session.CreateGuest();
        }
    }
}
