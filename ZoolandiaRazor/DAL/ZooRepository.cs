using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZoolandiaRazor.Models;

namespace ZoolandiaRazor.DAL
{
    public class ZooRepository
    {
        public ZooContext Context { get; set; }
        public ZooRepository(ZooContext context)
        {
            this.Context = context;
        }

        public List<Animal> GetAnimals()
        {
            throw new NotImplementedException();
        }

        public void AddAnimal(Animal newAnimal)
        {
            throw new NotImplementedException();
        }

        public Animal Find(string animalName)
        {
            throw new NotImplementedException();
        }

        public Animal Find(int animalId)
        {
            throw new NotImplementedException();
        }
    }
}