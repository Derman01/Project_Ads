using System;
using System.Collections.Generic;
using System.Security.Permissions;
using Project_Ads.Core;

namespace Project_Ads.MVVM.Model
{
    public class Advertisement: ObservableObject
    {
        public enum AdvertisementType
        {
            Lose,
            Find
        }
        
        public int RegNum { get; set; }
        
        public User User { get; set; }

        public Animal Animal { get; set; }

        private AdvertisementType _advType;
        
        private string _description;

        private string _address;

        private DateTime _dateEvent;

        private DateTime _dateCreate;

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
        
        
        private static Dictionary<AdvertisementType, string> StringFormatTypeAdvertisement = new Dictionary<AdvertisementType, string>()
        {
            {AdvertisementType.Find, "Находка"},
            {AdvertisementType.Lose , "Пропажа"}
        };
        private static Dictionary<AdvertisementType, string> StringFormatDateTypeAdvertisement = new Dictionary<AdvertisementType, string>()
        {
            {AdvertisementType.Find, "Дата находки"},
            {AdvertisementType.Lose , "Дата пропажи"}
        };
        
        public string GetStringTypeAdvertisement => StringFormatTypeAdvertisement[_advType];
        public string GetStringDateTypeAdvertisement => StringFormatDateTypeAdvertisement[_advType];
        
        public AdvertisementType Type
        {
            get => _advType;
            set
            {
                _advType = value; 
                OnPropertyChanged("Type"); 
                OnPropertyChanged("GetStringTypeAdvertisement");
            }
        }
        
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }
        
        public string Address
        {
            get => _address;
            set { _address = value; OnPropertyChanged("Address");}
        }
        
        public DateTime DateEvent
        {
            get => _dateEvent;
            set 
            { 
                _dateEvent = value;
                OnPropertyChanged("DateEvent"); 
                OnPropertyChanged("GetFormatStringDateType"); 
            }
        }
        
        public DateTime DateCreate
        {
            get => _dateCreate;
            set
            {
                _dateCreate = value;
                OnPropertyChanged("DateCreate");
                OnPropertyChanged("GetFormatStringDateCreate");
            }
        }
        
        public string GetFormatStringDateCreate => _dateCreate.ToString("d");

        public string DeleteImageUrl => $@"{App.PATH}\Icons\trash_24px.png";
        
        public string GetFormatStringDateType => _dateEvent.ToString("d");
    }
}