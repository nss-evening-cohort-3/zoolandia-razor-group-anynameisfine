using System;
using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZoolandiaRazor.DAL;
using Moq;
using ZoolandiaRazor.Models;
using System.Collections.Generic;
using System.Linq;

namespace ZoolandiaRazor.Tests
{
    [TestClass]
    public class ZooTests
    {
        Mock<ZooContext> mock_context { get; set; }
        Mock<DbSet<Animal>> mock_animal_table { get; set; }
        List<Animal> animalList { get; set; }
        ZooRepository repo { get; set; }

        public void ConnectMocksToDatastore()
        {
            var queryable_list = animalList.AsQueryable();

            mock_animal_table.As<IQueryable<Animal>>().Setup(m => m.Provider).Returns(queryable_list.Provider);
            mock_animal_table.As<IQueryable<Animal>>().Setup(m => m.Expression).Returns(queryable_list.Expression);
            mock_animal_table.As<IQueryable<Animal>>().Setup(m => m.ElementType).Returns(queryable_list.ElementType);
            mock_animal_table.As<IQueryable<Animal>>().Setup(m => m.GetEnumerator()).Returns(() => queryable_list.GetEnumerator());

            mock_context.Setup(c => c.Animals).Returns(mock_animal_table.Object);

            mock_animal_table.Setup(t => t.Add(It.IsAny<Animal>())).Callback((Animal a) => animalList.Add(a));
            mock_animal_table.Setup(t => t.Remove(It.IsAny<Animal>())).Callback((Animal a) => animalList.Remove(a));
            mock_animal_table.Setup(t => t.RemoveRange(It.IsAny<IEnumerable<Animal>>())).Callback((IEnumerable<Animal> a) => animalList.RemoveRange(0, a.Count<Animal>()));
        }

        [TestInitialize]
        public void Initialize()
        {
            mock_context = new Mock<ZooContext>();
            mock_animal_table = new Mock<DbSet<Animal>>();
            animalList = new List<Animal>();
            repo = new ZooRepository(mock_context.Object);
        }
        [TestCleanup]
        public void Teardown()
        {
            repo = null;
        }
        [TestMethod]
        public void RepoCanCreateInstance()
        {
            Assert.IsNotNull(repo);
        }
        [TestMethod]
        public void RepoHasContext()
        {
            Assert.IsNotNull(repo.Context);
        }
        [TestMethod]
        public void RepoHasNoAnimals()
        {
            ConnectMocksToDatastore();

            List<Animal> animals = repo.GetAnimals();
            int expected_animal_count = 0;
            int actual_animal_count = animals.Count;

            // Assert
            Assert.AreEqual(expected_animal_count, actual_animal_count);
        }
        [TestMethod]
        public void RepoCanAddAnimalToDatabase()
        {
            ConnectMocksToDatastore();
            Animal newAnimal = new Animal { AnimalId = 1, Name = "x", CommonName = "y", ScientificName = "z", HabitatId = 5, Age = 8 };

            repo.AddAnimal(newAnimal);

            int actual_animal_count = repo.GetAnimals().Count;
            int expected_animal_count = 1;

            Assert.AreEqual(expected_animal_count, actual_animal_count);
        }
        [TestMethod]
        public void RepoCanFindAnimal()
        {
            animalList.Add(new Animal { AnimalId = 1, Name = "x", CommonName = "y", ScientificName = "z", HabitatId = 5, Age = 8 });
            animalList.Add(new Animal { AnimalId = 2, Name = "a", CommonName = "b", ScientificName = "c", HabitatId = 3, Age = 20 });
            animalList.Add(new Animal { AnimalId = 3, Name = "q", CommonName = "r", ScientificName = "s", HabitatId = 8, Age = 305 });
            ConnectMocksToDatastore();

            int animalId = 2;
            Animal actual_animal = repo.Find(animalId);

            int expected_animal_id = 2;
            int actual_animal_id = actual_animal.AnimalId;
            Assert.AreEqual(expected_animal_id, actual_animal_id);

        }
        [TestMethod]
        public void RepoCanGetAllAnimals()
        {
            animalList.Add(new Animal { AnimalId = 1, Name = "x", CommonName = "y", ScientificName = "z", HabitatId = 5, Age = 8 });
            animalList.Add(new Animal { AnimalId = 2, Name = "a", CommonName = "b", ScientificName = "c", HabitatId = 3, Age = 20 });
            animalList.Add(new Animal { AnimalId = 3, Name = "q", CommonName = "r", ScientificName = "s", HabitatId = 8, Age = 305 });
            ConnectMocksToDatastore();

            List<Animal> expected_animal_list = new List<Animal>();
            expected_animal_list.Add(new Animal { AnimalId = 1, Name = "x", CommonName = "y", ScientificName = "z", HabitatId = 5, Age = 8 });
            expected_animal_list.Add(new Animal { AnimalId = 2, Name = "a", CommonName = "b", ScientificName = "c", HabitatId = 3, Age = 20 });
            expected_animal_list.Add(new Animal { AnimalId = 3, Name = "q", CommonName = "r", ScientificName = "s", HabitatId = 8, Age = 305 });

            List<Animal> actual_animal_list = repo.GetAnimals();

            Assert.AreEqual(expected_animal_list, actual_animal_list);


        }
    }
}
