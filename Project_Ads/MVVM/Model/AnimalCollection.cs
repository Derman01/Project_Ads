using System.Collections.Generic;
using Project_Ads.Core;
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

        public static List<Animal> GetAnimals()
        {
            var animals = Connection.ExecuteGetAnimalList(
                "SELECT a.id, a2.type, a.description, a.path FROM animal a INNER JOIN animal_type a2 on a2.id = a.type_id");
            Animals = animals; //проверить на правильность операции
            return animals;
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