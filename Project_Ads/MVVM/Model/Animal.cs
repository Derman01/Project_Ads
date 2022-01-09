using System.Collections.Generic;

namespace Project_Ads.Model
{
    public class Animal
    {
        public int Num { get; set; }
        public Types Type { get; set; }
        public Colors Color { get; set; }
        public string Pic { get; set; }
        public enum Types
        {
            Cat,
            Dog
        }
        public enum Colors
        {
            Blue, 
            Red,
            Black
        }
        public static Animal CreateAnimal(Colors animalColors, string pic, Types anTypes, int animalNum)
        {
            var animal = new Animal()
            {
                Pic = pic,
                Color = animalColors,
                Num = animalNum,
                Type = anTypes,
            };
            return animal;
        }
        public void EditAnimalData(Colors animalColor, string pic)
        {
            Color = animalColor;
            Pic = pic;
        }
        
        public static Dictionary<Colors, string> convertToColor = new Dictionary<Colors, string>()
        {
            { Colors.Black , "Черный"},
            { Colors.Blue , "Голубой"},
            { Colors.Red , "Рыжий"}
        };
        
        public static Dictionary<Animal.Types, string> convertToType = new Dictionary<Animal.Types, string>()
        {
            { Types.Cat , "Кот / Кошка"},
            { Types.Dog, "Собака / пёс"}
        };
    }
}