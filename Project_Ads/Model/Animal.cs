using System.Collections.Generic;

namespace Project_Ads.Model
{
    public static class Animal
    {
        public enum Type
        {
            Cat,
            Dog
        }
        
        public static Dictionary<Animal.Type, string> convertToType = new Dictionary<Animal.Type, string>()
        {
            { Type.Cat , "Кот / Кошка"},
            { Type.Dog, "Собака / пёс"}
        };
        
        public enum Color
        {
            Blue, 
            Red,
            Black
        }

        public static Dictionary<Color, string> convertToColor = new Dictionary<Color, string>()
        {
            { Color.Black , "Черный"},
            { Color.Blue , "Голубой"},
            { Color.Red , "Рыжий"}
        };
    }
}