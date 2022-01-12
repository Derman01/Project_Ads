using System;
using System.Collections.Generic;
using System.Security.Permissions;
using Project_Ads.Core;

namespace Project_Ads.MVVM.Model
{
    public class Advertisement: ObservableObject
    {
        public int RegNum { get; set; }
        
        public User User { get; set; }

        public Animal Animal { get; set; }

        public AdvertisementType Type { get; set; }
        
        public string Description { get; set; }

        public string Address { get; set; }

        public DateTime DateEvent { get; set; }

        public DateTime DateCreate { get; set; }

        public static Advertisement CreateAdv(
            User user, string address, string description, DateTime dateCreate,
            DateTime dateEvent, AdvertisementType advAdvertisementType, int regNum, Animal animal)
        {
            return new Advertisement(user, address, description, dateCreate,
                dateEvent, advAdvertisementType, regNum, animal);
        }

        private Advertisement( User user, string address, string description, DateTime dateCreate,
            DateTime dateEvent, AdvertisementType advAdvertisementType, int regNum, Animal animal)
        {
            User = user;
            Address = address;
            Description = description;
            DateCreate = dateCreate;
            DateEvent = dateEvent;
            Type = advAdvertisementType;
            RegNum = regNum;
            Animal = animal;
        }

        public void EditAdvData(
            string address, string description, DateTime dateEvent, Animal editedAnimal)
        {
            Address = address;
            Description = description;
            DateEvent = dateEvent;
            Animal = editedAnimal;
        }
        
        public enum AdvertisementType
        {
            Lose,
            Find
        }
    }
}