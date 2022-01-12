using System;
using System.Collections.Generic;
using Project_Ads.MVVM.Model;

namespace Project_Ads.MVVM.View
{
    public class AdvertisementData
    {
        public int RegNum { get; }
        public AdvertisementType AdvType { get; }
        public string Address { get; }
        public string Description { get; }
        public DateTime DateEvent { get; }
        public DateTime DateCreate { get; }
        public int AnNum { get; }
        public string AnPic { get; }
        public AnimalType AnType { get; }
        public string AnColor { get; }
        public string Phone { get; }

        public AdvertisementData(
            int regNum, int advType, string address, string description, DateTime dateEvent, DateTime dateCreate,
            int anNum, string anPic, int anType, string anColor, string phone)
        {
            RegNum = regNum;
            AdvType = advType == 0 ? AdvertisementType.Lose : AdvertisementType.Find;
            Address = address;
            Description = description;
            DateEvent = dateEvent;
            DateCreate = dateCreate;
            AnNum = anNum;
            AnPic = anPic;
            AnType = anType == 0 ? AnimalType.Cat : AnimalType.Dog;
            AnColor = anColor;
            Phone = phone;
        }
        
        public enum AdvertisementType
        {
            Lose,
            Find
        }
        
        public enum AnimalType
        {
            Cat,
            Dog
        }

        public string GetStringTypeAnimal => convertToType[AnType];
        public string GetStringTypeAdvertisement => _stringFormatTypeAdvertisement[AdvType];
        public string GetStringDateTypeAdvertisement => _stringFormatDateTypeAdvertisement[AdvType];
        
        public string DeleteImageUrl => $@"{App.PATH}\Icons\trash_24px.png";
        
        public string GetFormatStringDateCreate => DateCreate.ToString("d");
        
        public string GetFormatStringDateEvent => DateEvent.ToString("d");
        
        private static Dictionary<AnimalType, string> convertToType = new Dictionary<AnimalType, string>()
        {
            { AnimalType.Cat , "Кот / Кошка"},
            { AnimalType.Dog, "Собака / пёс"}
        };

        private static Dictionary<AdvertisementType, string> _stringFormatTypeAdvertisement = new Dictionary<AdvertisementType, string>()
        {
            {AdvertisementType.Find, "Находка"},
            {AdvertisementType.Lose , "Пропажа"}
        };
        private static Dictionary<AdvertisementType, string> _stringFormatDateTypeAdvertisement = new Dictionary<AdvertisementType, string>()
        {
            {AdvertisementType.Find, "Дата находки"},
            {AdvertisementType.Lose , "Дата пропажи"}
        };
    }
}