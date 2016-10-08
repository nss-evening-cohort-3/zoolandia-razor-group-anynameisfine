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

        public Animal FindAnimal(int animalId)
        {
            throw new NotImplementedException();
        }

        public List<Habitat> GetHabitats()
        {
            throw new NotImplementedException();
        }

        public void AddHabitat(Habitat newHabitat)
        {
            throw new NotImplementedException();
        }

        public Habitat FindHabitat(int habitatId)
        {
            throw new NotImplementedException();
        }

        public List<Employee> GetEmployees()
        {
            throw new NotImplementedException();
        }

        public void AddEmployee(Employee newEmployee)
        {
            throw new NotImplementedException();
        }

        public Employee FindEmployee(int employeeId)
        {
            throw new NotImplementedException();
        }
    }
}