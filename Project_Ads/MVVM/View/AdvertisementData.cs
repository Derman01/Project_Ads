using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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
        
        public ImageSource GetSourceImage => ConvertToImage(AnPic);
        
        public static string ImageToBase64(BitmapImage image)
        {
            //проверяем параметр
            if (image == null) throw new ArgumentException($"{nameof(image)} не может пустым");

            //запоминать будем jpeg
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            //содержимое картинки
            encoder.Frames.Add(BitmapFrame.Create(image));

            byte[] imageBytes = null;
            using (MemoryStream ms = new MemoryStream())
            {
                //пишем в поток
                encoder.Save(ms);
                //поток в массив байт
                imageBytes = ms.ToArray();
            }

            //массив байт конвертируем в строку
            string result = Convert.ToBase64String(imageBytes);

            //отдаем результат
            return result;
        }
        
        public static BitmapImage ConvertToImage(string base64String)
        { 
            if (String.IsNullOrEmpty(base64String)) throw new ArgumentException($"{nameof(base64String)} не может быть пустым");

            //Конвертация Base64 String в byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);

            BitmapImage result = new BitmapImage();
            using (MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                //читаем картинку из потока
                result.BeginInit();
                result.CacheOption = BitmapCacheOption.OnLoad;
                result.StreamSource = ms;
                result.EndInit();
            }

            return result;
        }
        
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