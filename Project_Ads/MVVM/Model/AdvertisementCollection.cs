using System;
using System.Collections.Generic;
using System.Linq;
using Project_Ads.Core;
using Project_Ads.Model;

namespace Project_Ads.MVVM.Model
{
    public class AdvertisementCollection
    {
        private static List<Advertisement> Advertisements;

        private static void AddAdv(Advertisement adv)
        {
            Advertisements.Add(adv);
        }

        private static void UpdateAdvs(Advertisement advertisement)
        {
            Advertisements = Advertisements.Where(adv => adv.RegNum != advertisement.RegNum).ToList();
            Advertisements.Add(advertisement);
        }

        private static void DeleteAdv(Advertisement advertisement)
        {
            Advertisements = Advertisements.Where(adv => adv.RegNum != advertisement.RegNum).ToList();
        }

        public static void CreateAdvertisements(
            User user, Advertisement.Type advType, string address, string description,
            DateTime dateEvent, Animal.Types anType, Animal.Colors animalColor, string pic)
        {
            var animal = AnimalCollection.CreateAnimal(anType, animalColor, pic);
            int regNum = 0;//Connection.Execute('SELECT reg_num FROM Advertisement ORDER BY reg_num DESC LIMIT 1')
            var advertisement = Advertisement.CreateAdv(user, address, description,
                DateTime.Now, dateEvent, advType, regNum, animal);
            AddAdv(advertisement);
        }

        public static List<Advertisement> GetAdvertisementList()
        {
            return Advertisements;
        }

        public static List<Advertisement> GetUserAdvertisementList(User user)
        {
            return Advertisements.Where(ad => ad.User == user).ToList();
        }

        public static Advertisement OpenAdvertisement(int regNum)
        {
            return Advertisements.Find(advertisement => advertisement.RegNum == regNum);
        }

        public static void EditAdvertisement(
            int regNum, string address, string description, DateTime dateEvent,
            Animal.Colors animalColor, string pic)
        {
            var advertisement = Advertisements.Find(adv => adv.RegNum == regNum);
            var animalNum = advertisement.Animal.Num;
            var editedAnimal = AnimalCollection.EditAnimalData(animalNum, animalColor, pic);
            var editedAdv = advertisement.EditAdvData(address, description, dateEvent, editedAnimal);
            UpdateAdvs(editedAdv);
        }

        public static void DeleteAdvertisement(int regNum)
        {
            var adv = Advertisements.Find(advertisement => advertisement.RegNum == regNum);
            DeleteAdv(adv);
        }
    }
}