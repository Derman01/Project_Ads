using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Project_Ads.Core;

namespace Project_Ads.MVVM.Model
{
    public static class AdvertisementCollection
    {
        private static ObservableCollection<Advertisement> Advertisements = new ObservableCollection<Advertisement>();

        private static void UpdateAdv(Advertisement advertisement)
        {
            var index = Advertisements.IndexOf(adv=> adv.RegNum == advertisement.RegNum);
            Advertisements[index] = advertisement;
        }

        public static void CreateAdvertisements(
            User user, Advertisement.AdvertisementType advAdvertisementType, string address, string description,
            DateTime dateEvent, Animal.Types anType, string animalColor, string pic)
        {
            var animal = AnimalCollection.CreateAnimal(anType, animalColor, pic);
            int regNum = Connection.ExecuteGetLastRegNum("SELECT reg_num FROM advertisement ORDER BY reg_num DESC LIMIT 1");
            var advertisement = Advertisement.CreateAdv(user, address, description,
                DateTime.Now, dateEvent, advAdvertisementType, regNum, animal);
            Advertisements.Add(advertisement);
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


        public static ObservableCollection<Advertisement> GetAdvertisementList
        {
            get
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
        }

        public static ObservableCollection<Advertisement> GetAdvertisementsByUser
        {
            get
            {
                var _list = Advertisements.Where(ad => ad.User == Session.currentUser).ToArray(); 
                var advs = new ObservableCollection<Advertisement>();
                advs.CopyTo(_list, 0);
                return advs;
            }
        }

        public static Advertisement GetAdvertisement(int regNum)
        {
            return Advertisements.FirstOrDefault(advertisement => advertisement.RegNum == regNum);
        }

        public static void EditAdvertisement(
            int regNum, string address, string description, DateTime dateEvent,
            string animalColor, string pic)
        {
            var advertisement = Advertisements.First(adv => adv.RegNum == regNum);
            var animalNum = advertisement.Animal.Num;
            var editedAnimal = AnimalCollection.EditAnimalData(animalNum, animalColor, pic);
            advertisement.EditAdvData(address, description, dateEvent, editedAnimal);
            UpdateAdv(advertisement);
            
            var advData = new Tuple<string, object>[]
            {
                new Tuple<string, object>("address", address),
                new Tuple<string, object>("description", description),
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
            var adv = Advertisements.FirstOrDefault(advertisement => advertisement.RegNum == regNum);
            Advertisements.Remove(adv);
            
            var data = new [] {new Tuple<string, object>("regNum", regNum)};
            Connection.ExecuteNonQuery(
                "DELETE FROM advertisement WHERE reg_num = @regNum",
                data);
        }
    }

    public static class Extensiton
    {
        public static int IndexOf<T>(this ObservableCollection<T> _list, Func<T, bool> condition)
        {
            var index = 0;
            foreach (var e in _list)
            {
                if (condition(e))
                    return index;
                index++;
            }

            return -1;
        }
    }
}