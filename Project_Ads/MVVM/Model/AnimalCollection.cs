using System.Collections.Generic;
using System.Linq;
using Project_Ads.Core;

namespace Project_Ads.MVVM.Model
{
    public class AnimalCollection
    {
        private static List<Animal> Animals = new List<Animal>();

        private static void AddAnimal(Animal animal)
        {
            Animals.Add(animal);
        }

        private static void UpdateAnimal(Animal animal)
        {
            var index = Animals.FindIndex(a => a.Num == animal.Num);
            Animals[index] = animal;
        }

        public static List<Animal> GetAnimals()
        {
            var animals = Connection.ExecuteGetAnimalList();
            Animals = animals; //проверить на правильность операции
            return animals;
        }
        
        public static Animal CreateAnimal(Animal.Types anType, string animalColor, string pic, int animalNum)
        {
            var animal = Animal.CreateAnimal(animalColor, pic, anType, animalNum);
            AddAnimal(animal);
            return animal;
        }

        public static Animal EditAnimalData(int animalNum, string animalColor, string pic)
        {
            var animal = Animals.First(a=> a.Num == animalNum);
            animal.EditAnimalData(animalColor, pic);
            UpdateAnimal(animal);
            return animal;
        }
        
    }
}