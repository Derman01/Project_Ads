using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Project_Ads.Core;
using Project_Ads.MVVM.View;

namespace Project_Ads.MVVM.Model
{
    public class AdvertisementCollection
    {
        private static List<Advertisement> Advertisements = new List<Advertisement>();

        private static void UpdateAdv(Advertisement advertisement)
        {
            var index = Advertisements.FindIndex(adv=> adv.RegNum == advertisement.RegNum);
            Advertisements[index] = advertisement;
        }
        
        private static List<Dictionary<string, object>> ConvertToDictionaryList(IEnumerable<Advertisement> advs)
        {
            var list = new List<Dictionary<string, object>>();
            foreach (var advertisement in advs)
            {
                var dict = CreateAdvertisementDictionary(advertisement);
                list.Add(dict);
            }
            return list;
        }

        private static Dictionary<string, object> CreateAdvertisementDictionary(Advertisement advertisement)
        {
            var dict = new Dictionary<string, object>
            {
                ["regNum"] = advertisement.RegNum,
                ["anNum"] = advertisement.Animal.Num,
                ["pic"] = advertisement.Animal.Pic,
                ["advType"] = Convert.ToInt32(advertisement.Type),
                ["anType"] = Convert.ToInt32(advertisement.Animal.Type),
                ["anColor"] = advertisement.Animal.Color,
                ["address"] = advertisement.Address,
                ["dateEvent"] = advertisement.DateEvent,
                ["phone"] = advertisement.User.UserPhone,
                ["description"] = advertisement.Description,
                ["dateCreate"] = advertisement.DateCreate
            };
            return dict;
        }

        public static List<Dictionary<string, object>> CreateAdvertisements(
            User user, int advertisementType, string address, string description,
            DateTime dateEvent, int animalType, string animalColor, string pic)
        {
            var adType = advertisementType == 0
                ? Advertisement.AdvertisementType.Lose
                : Advertisement.AdvertisementType.Find;
            var anType = animalType == 0 ? Animal.Types.Cat : Animal.Types.Dog;
            
            var animalId = Connection.ExecuteCreateAnimal((int)anType, animalColor, pic);
            var animal = AnimalCollection.CreateAnimal(anType, animalColor, pic, animalId);
            int regNum = Connection.ExecuteGetLastRegNum();
            var advertisement = Advertisement.CreateAdv(user, address, description,
                DateTime.Now, dateEvent, adType, regNum, animal);
            Advertisements.Add(advertisement);
            Connection.ExecuteCreateAdvertisement(
                advertisement.User.UserId, advertisement.Type.ToString(), advertisement.Address, advertisement.Animal.Num, 
                advertisement.Description, advertisement.DateEvent, advertisement.DateCreate);
            return ConvertToDictionaryList(Advertisements);
        }


        public static List<Dictionary<string, object>> GetAdvertisementList
        {
            get
            {
                if (Advertisements.Count != 0)
                    return ConvertToDictionaryList(Advertisements);
                var animals = AnimalCollection.GetAnimals();
                var advs = Connection.ExecuteGetAdvertisementList(animals);
                Advertisements = advs;
                return ConvertToDictionaryList(advs);
            }
        }

        public static List<Dictionary<string, object>> GetAdvertisementsByUser(User user)
        {
            var sortedList = Advertisements.Where(ad => ad.User.UserId == user.UserId).ToArray(); 
            return ConvertToDictionaryList(sortedList);
        }

        public static Dictionary<string, object> GetAdvertisement(int regNum)
        {
            return CreateAdvertisementDictionary(
                Advertisements.FirstOrDefault(advertisement => advertisement.RegNum == regNum));
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
            Connection.ExecuteEditAdvertisement(address, description, dateEvent, regNum, 
                animalColor, pic, animalNum);
        }

        public static void DeleteAdvertisement(int regNum)
        {
            var adv = Advertisements.FirstOrDefault(advertisement => advertisement.RegNum == regNum);
            Advertisements.Remove(adv);
            Connection.ExecuteDeleteAdvertisement(regNum, DateTime.Now);
        }
    }

    public static class Extensiton
    {
        public static int IndexOf<T>(this List<T> _list, Func<T, bool> condition)
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