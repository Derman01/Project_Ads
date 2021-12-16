using System;
using System.Collections.Generic;
using System.Security.Permissions;
using Project_Ads.Core;

namespace Project_Ads.Model
{
    public class Advertisement: ObservableObject
    {
        public enum Type
        {
            Lose,
            Find
        }

        public static Dictionary<Advertisement.Type, string> StringFormatTypeAdvertisement = new Dictionary<Type, string>()
        {
            {Type.Find, "Находка"},
            {Type.Lose , "Пропажа"}
        };

        public static Dictionary<Advertisement.Type, string> StringFormatDateTypeAdvertisement = new Dictionary<Type, string>()
        {
            {Type.Find, "Дата находки"},
            {Type.Lose , "Дата пропажи"}
        };

        public int Id { get; set; }

        private Advertisement.Type _typeAdvertisement;
        public Advertisement.Type TypeAdvertisement
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



        private Animal.Type _typeAnimal;
        public string GetStringTypeAnimal => Animal.convertToType[_typeAnimal];
        public Animal.Type TypeAnimal
        {
            get => _typeAnimal;
            set
            {
                _typeAnimal = value;
                OnPropertyChanged("TypeAnimal");
                OnPropertyChanged($"GetStringTypeAnimal");
            }
        }

        
        private Animal.Color _colorAnimal;
        public string GetStringColorAnimal => Animal.convertToColor[_colorAnimal];
        public Animal.Color ColorAnimal
        {
            get => _colorAnimal;
            set
            {
                _colorAnimal = value;  
                OnPropertyChanged("GetStringColorAnimal");
                OnPropertyChanged("ColorAnimal");
            }
        }

        private string _locationFind;
        public string LocationFind
        {
            get => _locationFind;
            set { _locationFind = value; OnPropertyChanged("LocationFind");}
        }

        private DateTime _typeDate;
        public DateTime DateType
        {
            get => _typeDate;
            set 
            { 
                _typeDate = value;
                OnPropertyChanged("DateType"); 
                OnPropertyChanged("GetFormatStringDateType"); 
            }
        }
        public string GetFormatStringDateType => _typeDate.ToString("d");

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


        private string _marks; 
        
        /// <summary>
        /// Приметы
        /// </summary>
        public string Marks
        {
            get => _marks;
            set { _marks = value;  OnPropertyChanged("Marks");}
        }

        private string _phone;
        public string Phone
        {
            get => _phone;
            set { _phone = value; OnPropertyChanged("Phone");}
        }

        private string _imageUrl = @$"{App.PATH}\Icons\icons8_image_120px_3.jpg";
        public string ImageUrl
        {
            get => _imageUrl;
            set { _imageUrl = App.PATH + value; OnPropertyChanged("ImageUrl"); }
        }

        public string DeleteImageUrl => $@"{App.PATH}\Icons\trash_24px.png";

    }
}