using System;
using System.Collections.Generic;
using System.Security.Permissions;
using Project_Ads.Core;

namespace Project_Ads.Model
{
    public class Advertisement: ObservableObject
    {
        public int RegNum { get; set; }
        
        public User User { get; set; }

        public Animal Animal { get; set; }
        public enum Type
        {
            Lose,
            Find
        }

        public static Dictionary<Type, string> StringFormatTypeAdvertisement = new Dictionary<Type, string>()
        {
            {Type.Find, "Находка"},
            {Type.Lose , "Пропажа"}
        };

        public static Dictionary<Type, string> StringFormatDateTypeAdvertisement = new Dictionary<Type, string>()
        {
            {Type.Find, "Дата находки"},
            {Type.Lose , "Дата пропажи"}
        };

        

        private Type _typeAdvertisement;
        public Type TypeAdvertisement
        {
            get => _typeAdvertisement;
            set
            {
                _typeAdvertisement = value; 
                OnPropertyChanged("TypeAdvertisement"); 
                OnPropertyChanged("GetStringTypeAdvertisement");
            }
        }
        public string GetStringTypeAdvertisement => StringFormatTypeAdvertisement[_typeAdvertisement];
        public string GetStringDateTypeAdvertisement => StringFormatDateTypeAdvertisement[_typeAdvertisement];
        
        private Animal.Types _typesAnimal;
        public string GetStringTypeAnimal => Animal.convertToType[_typesAnimal];
        public Animal.Types TypesAnimal
        {
            get => _typesAnimal;
            set
            {
                _typesAnimal = value;
                OnPropertyChanged("TypesAnimal");
                OnPropertyChanged($"GetStringTypeAnimal");
            }
        }

        
        private string _description;

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        private string _address;
        public string Address
        {
            get => _address;
            set { _address = value; OnPropertyChanged("Address");}
        }

        private DateTime _dateEvent;
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
        public string GetFormatStringDateType => _dateEvent.ToString("d");

        private DateTime _dateCreate;
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

        public static Advertisement CreateAdv(
            User user, string address, string description, DateTime dateCreate,
            DateTime dateEvent, Type advType, int regNum, Animal animal)
        {
            var adv = new Advertisement()
            {
                User = user,
                Address = address,
                Description = description,
                DateCreate = dateCreate,
                DateEvent = dateEvent,
                TypeAdvertisement = advType,
                RegNum = regNum,
                Animal = animal,
            };
            return adv;
        }

        public Advertisement EditAdvData(
            string address, string description, DateTime dateEvent, Animal editedAnimal)
        {
            Address = address;
            Description = description;
            DateEvent = dateEvent;
            Animal = editedAnimal;
            return this;
        }

    }
}