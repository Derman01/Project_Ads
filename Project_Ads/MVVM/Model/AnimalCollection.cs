using System.Collections.Generic;
using System.Linq;
using Project_Ads.Core;

namespace Project_Ads.MVVM.Model
{
    public class AnimalCollection
    {
        private static List<Animal> Animals = new List<Animal>();

        private static void UpdateAnimals(Animal animal)
        {
            var index = Animals.FindIndex(a => a.Num == animal.Num);
            Animals[index] = animal;
        }

        public static List<Animal> GetAnimals()
        {
            var animals = Connection.ExecuteGetAnimalList();
            foreach (var animal in animals)
                Animals.Add(animal);
            return Animals;
        }
        
        public static Animal CreateAnimal(int anType, string animalColor, string pic)
        {
            var animalNum = Connection.ExecuteCreateAnimal(anType, animalColor, pic);
            var animal = Animal.CreateAnimal(animalColor, pic, anType, animalNum);
            Animals.Add(animal);
            return animal;
        }

        public static Animal EditAnimalData(int animalNum, string animalColor, string pic)
        {
            var animal = Animals.First(a=> a.Num == animalNum);
            animal.EditAnimalData(animalColor, pic);
            UpdateAnimals(animal);
            return animal;
        }
        
    }
}