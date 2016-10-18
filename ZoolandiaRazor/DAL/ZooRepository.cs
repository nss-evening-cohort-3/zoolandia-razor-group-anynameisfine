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
        public ZooRepository()
        {
            Context = new ZooContext();
        }

        public ZooRepository(ZooContext context)
        {
            this.Context = context;
        }

        public List<Animal> GetAnimals()
        {
            return Context.Animals.ToList();
        }

        public void AddAnimal(Animal newAnimal)
        {
            Context.Animals.Add(newAnimal);
            Context.SaveChanges();
        }

        public Animal FindAnimal(int animalId)
        {
            Animal foundIt = Context.Animals.FirstOrDefault(a => a.AnimalId == animalId);
            return foundIt;
        }

        public List<Habitat> GetHabitats()
        {
            return Context.Habitats.ToList();
        }

        public void AddHabitat(Habitat newHabitat)
        {
            Context.Habitats.Add(newHabitat);
            Context.SaveChanges();
        }

        public Habitat FindHabitat(int habitatId)
        {
            Habitat foundIt = Context.Habitats.FirstOrDefault(a => a.HabitatId == habitatId);
            return foundIt;
        }

        public List<Employee> GetEmployees()
        {
            return Context.Employees.ToList();
        }

        public void AddEmployee(Employee newEmployee)
        {
            Context.Employees.Add(newEmployee);
            Context.SaveChanges();
        }

        public Employee FindEmployee(int employeeId)
        {
           Employee foundIt = Context.Employees.FirstOrDefault(a => a.EmployeeId == employeeId);
            return foundIt;
        }
    }
}