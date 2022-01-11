using System.Collections.Generic;

namespace Project_Ads.MVVM.Model
{
    public class Animal
    {
        public int Num { get; set; }
        public Types Type { get; set; }
        public string Color { get; set; }
        public string Pic { get; set; }
        public enum Types
        {
            Cat,
            Dog
        }
        public static Animal CreateAnimal(string animalColors, string pic, Types anTypes, int animalNum)
        {
            return new Animal()
            {
                Pic = pic,
                Color = animalColors,
                Num = animalNum,
                Type = anTypes,
            };
        }
        public void EditAnimalData(string animalColor, string pic)
        {
            Color = animalColor;
            Pic = pic;
        }
        
        private static Dictionary<Animal.Types, string> convertToType = new Dictionary<Animal.Types, string>()
        {
            { Types.Cat , "Кот / Кошка"},
            { Types.Dog, "Собака / пёс"}
        };
        public string GetStringType => convertToType[this.Type];
    }
}