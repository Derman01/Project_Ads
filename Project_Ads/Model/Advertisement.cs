using System;
using System.Collections.Generic;
using Project_Ads.Core;

namespace Project_Ads.Model
{
    public class Advertisement: ObservableObject
    {
        private static string PATH = Environment.CurrentDirectory + "/../../../Properties";
        public enum Type
        {
            Lose,
            Find
        }

        public static Dictionary<Advertisement.Type, string> StringFormatTypeAdvertisement = new Dictionary<Type, string>()
        {
            {Type.Find, "Находка"},
            { Type.Lose , "Пропажа"}
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

        private DateTime _dateFind;
        public DateTime DateFind
        {
            get => _dateFind;
            set 
            { 
                _dateFind = value;
                OnPropertyChanged("DateFind"); 
                OnPropertyChanged("GetFormatStringDateFind"); 
            }
        }
        public string GetFormatStringDateFind => _dateFind.ToString("d");
        

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

        private string _imageUrl = $"{PATH}/Icons/icons8_image_120px_3.jpg";
        public string ImageUrl
        {
            get => _imageUrl;
            set { _imageUrl = PATH + value; OnPropertyChanged("ImageUrl"); }
        }
    }
}