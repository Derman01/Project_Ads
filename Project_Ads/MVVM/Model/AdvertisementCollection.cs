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
            User user, Advertisement.AdvertisementType advAdvertisementType, string address, string description,
            DateTime dateEvent, Animal.Types anType, Animal.Colors animalColor, string pic)
        {
            var animal = AnimalCollection.CreateAnimal(anType, animalColor, pic);
            int regNum = Connection.ExecuteGetLastRegNum("SELECT reg_num FROM advertisement ORDER BY reg_num DESC LIMIT 1");
            var advertisement = Advertisement.CreateAdv(user, address, description,
                DateTime.Now, dateEvent, advAdvertisementType, regNum, animal);
            AddAdv(advertisement);
            var data = new []
            {
                new Tuple<string, object>("id_user", advertisement.User.UserId),
                new Tuple<string, object>("type", advertisement.Type.ToString()),
                new Tuple<string, object>("address", advertisement.Address),
                new Tuple<string, object>("id_animal", advertisement.Animal.Num),
                new Tuple<string, object>("description", advertisement.Description),
                new Tuple<string, object>("date_event", advertisement.DateEvent),
                new Tuple<string, object>("date_create", advertisement.DateCreate)
            };
            Connection.ExecuteNonQuery(
                "INSERT INTO advertisement VALUES (@id_user, @type, @address, @id_animal, @description, @date_event, @date_create)",
                data);
        }

        public static List<Advertisement> GetAdvertisementList()
        {
            if (Advertisements.Count != 0)
                return Advertisements;
            var animals = AnimalCollection.GetAnimals();
            var advs = Connection.ExecuteGetAdvertisementList(
                "SELECT a.reg_num, a.id_user, a.type, a.address, a.description, a.date_event, a.date_create, a.id_animal, u.phone FROM advertisement a INNER JOIN \"user\" u on u.id = a.id_user WHERE a.date_remove IS NOT NULL",
                animals);
            Advertisements = advs;
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
            advertisement.EditAdvData(address, description, dateEvent, editedAnimal);
            UpdateAdvs(advertisement);
            
            var advData = new Tuple<string, object>[]
            {
                new Tuple<string, object>("address", address),
                new Tuple<string, object>("dewscription", description),
                new Tuple<string, object>("dateEvent", dateEvent),
                new Tuple<string, object>("regNum", regNum),
            };
            Connection.ExecuteNonQuery(
                "UPDATE advertisement SET address = @address, description = @description, date_event = @dateEvent WHERE reg_num = @regNum",
                advData);
            var anData = new Tuple<string, object>[]
            {
                new Tuple<string, object>("animalColor", animalColor.ToString()),
                new Tuple<string, object>("pic", pic),
                new Tuple<string, object>("num", animalNum)
            };
            Connection.ExecuteNonQuery(
                "UPDATE animal SET description = @animalColor, path = @pic WHERE id = @num",
                anData);
        }

        public static void DeleteAdvertisement(int regNum)
        {
            var adv = Advertisements.Find(advertisement => advertisement.RegNum == regNum);
            DeleteAdv(adv);
            
            var data = new [] {new Tuple<string, object>("regNum", regNum)};
            Connection.ExecuteNonQuery(
                "DELETE FROM advertisement WHERE reg_num = @regNum",
                data);
        }
    }
}