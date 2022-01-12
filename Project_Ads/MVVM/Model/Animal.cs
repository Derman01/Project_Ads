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
        public static Animal CreateAnimal(string animalColor, string pic, int anType, int animalNum)
        {
            return new Animal()
            {
                Pic = pic,
                Color = animalColor,
                Num = animalNum,
                Type = anType == 0 ? Types.Cat : Types.Dog
            };
        }
        public void EditAnimalData(string animalColor, string pic)
        {
            Color = animalColor;
            Pic = pic;
        }
    }
}