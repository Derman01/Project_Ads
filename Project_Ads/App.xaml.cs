﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public static NpgsqlConnection Conn = new NpgsqlConnection(Connection.ConnString);

        public static void CreateAdvertisements(
            User user, Advertisement.AdvertisementType advAdvertisementType, string address, string description,
            DateTime dateEvent, Animal.Types anType, Animal.Colors animalColor, string pic)
        {
            AdvertisementCollection.CreateAdvertisements(user, advAdvertisementType, address, description, dateEvent, anType, animalColor, pic);
        }

        public static ObservableCollection<Advertisement> GetAdvertisementList => AdvertisementCollection.GetAdvertisementList();
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

        public static void Authorization(string login, string password)
        {
            Session.Authorize(login, password);
        }

        public static void Registration(string login, string password, string userName, string phone)
        {
            Session.Registrate(login, password, userName, phone);
        }
    }
}
