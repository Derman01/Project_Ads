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
            var animalId = Connection.ExecuteCreateAnimal(
                "INSERT INTO animal (type_id, description, path) VALUES (@type_id, @description, @path) RETURNING id",
                (int)anType, animalColor, pic);
            var animal = AnimalCollection.CreateAnimal(anType, animalColor, pic, animalId);
            int regNum = Connection.ExecuteGetLastRegNum("SELECT reg_num FROM advertisement ORDER BY reg_num DESC LIMIT 1");
            var advertisement = Advertisement.CreateAdv(user, address, description,
                DateTime.Now, dateEvent, advAdvertisementType, regNum, animal);
            Advertisements.Add(advertisement);
            
            Connection.ExecuteCreateAdvertisement(
                "INSERT INTO advertisement (id_user, type, address, id_animal, description, date_event, date_create) VALUES (@id_user, @type, @address, @id_animal, @description, @date_event, @date_create)",
                advertisement.User.UserId, advertisement.Type.ToString(), advertisement.Address, advertisement.Animal.Num,
                advertisement.Description, advertisement.DateEvent, advertisement.DateCreate);
        }


        public static ObservableCollection<Advertisement> GetAdvertisementList
        {
            get
            {
                if (Advertisements.Count != 0)
                    return Advertisements;
                var animals = AnimalCollection.GetAnimals();
                var advs = Connection.ExecuteGetAdvertisementList(
                    "SELECT a.reg_num, a.id_user, a.type, a.address, a.description, a.date_event, a.date_create, a.id_animal, u.phone FROM advertisement a INNER JOIN \"user\" u on u.id = a.id_user WHERE a.date_remove IS NULL",
                    animals);
                Advertisements = advs;
                return Advertisements;
            }
        }

        public static ObservableCollection<Advertisement> GetAdvertisementsByUser(User user)
        {
            var sortedList = Advertisements.Where(ad => ad.User.UserId == user.UserId).ToArray(); 
            var advs = new ObservableCollection<Advertisement>();
            foreach (var advertisement in sortedList)
                advs.Add(advertisement);
            return advs;
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
            Connection.ExecuteEditAdvertisement(
                "UPDATE advertisement SET address = @address, description = @description, date_event = @date_event WHERE reg_num = @reg_num",
                "UPDATE animal SET description = @animalColor, path = @pic WHERE id = @num",
                address, description, dateEvent, regNum, animalColor, pic, animalNum);
        }

        public static void DeleteAdvertisement(int regNum)
        {
            var adv = Advertisements.FirstOrDefault(advertisement => advertisement.RegNum == regNum);
            Advertisements.Remove(adv);
            
            Connection.ExecuteDeleteAdvertisement(
                "DELETE FROM advertisement WHERE reg_num = @reg_num",
                regNum);
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