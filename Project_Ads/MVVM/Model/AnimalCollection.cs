using System.Collections.Generic;
using Project_Ads.Model;

namespace Project_Ads.MVVM.Model
{
    public class AnimalCollection
    {
        private static List<Animal> Animals;

        private static void AddAnimal(Animal animal)
        {
            Animals.Add(animal);
        }

        private static void UpdateAnimal(Animal animal)
        {
            Animals[animal.Num] = animal;
        }

        public static Animal CreateAnimal(Animal.Types anType, Animal.Colors animalColor, string pic)
        {
            int animalNum = Animals.Count;
            var animal = Animal.CreateAnimal(animalColor, pic, anType, animalNum);
            AddAnimal(animal);
            return animal;
        }

        public static Animal EditAnimalData(int animalNum, Animal.Colors animalColor, string pic)
        {
            var animal = Animals[animalNum];
            animal.EditAnimalData(animalColor, pic);
            UpdateAnimal(animal);
            return animal;
        }
        
    }
}