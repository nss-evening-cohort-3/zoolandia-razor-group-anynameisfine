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
        Mock<DbSet<Habitat>> mock_habitat_table { get; set; }
        List<Habitat> habitatList { get; set; }
        List<Animal> animalList { get; set; }
        ZooRepository repo { get; set; }

        public void ConnectMocksToDatastore()
        {
            var queryable_animal_list = animalList.AsQueryable();
            var queryable_habitat_list = habitatList.AsQueryable();

            mock_animal_table.As<IQueryable<Animal>>().Setup(m => m.Provider).Returns(queryable_animal_list.Provider);
            mock_animal_table.As<IQueryable<Animal>>().Setup(m => m.Expression).Returns(queryable_animal_list.Expression);
            mock_animal_table.As<IQueryable<Animal>>().Setup(m => m.ElementType).Returns(queryable_animal_list.ElementType);
            mock_animal_table.As<IQueryable<Animal>>().Setup(m => m.GetEnumerator()).Returns(() => queryable_animal_list.GetEnumerator());

            mock_animal_table.As<IQueryable<Habitat>>().Setup(m => m.Provider).Returns(queryable_habitat_list.Provider);
            mock_animal_table.As<IQueryable<Habitat>>().Setup(m => m.Expression).Returns(queryable_habitat_list.Expression);
            mock_animal_table.As<IQueryable<Habitat>>().Setup(m => m.ElementType).Returns(queryable_habitat_list.ElementType);
            mock_animal_table.As<IQueryable<Habitat>>().Setup(m => m.GetEnumerator()).Returns(() => queryable_habitat_list.GetEnumerator());

            mock_context.Setup(c => c.Animals).Returns(mock_animal_table.Object);
            mock_context.Setup(c => c.Habitats).Returns(mock_habitat_table.Object);

            mock_animal_table.Setup(t => t.Add(It.IsAny<Animal>())).Callback((Animal a) => animalList.Add(a));
            mock_animal_table.Setup(t => t.Remove(It.IsAny<Animal>())).Callback((Animal a) => animalList.Remove(a));
            mock_animal_table.Setup(t => t.RemoveRange(It.IsAny<IEnumerable<Animal>>())).Callback((IEnumerable<Animal> a) => animalList.RemoveRange(0, a.Count<Animal>()));

            mock_habitat_table.Setup(t => t.Add(It.IsAny<Habitat>())).Callback((Habitat a) => habitatList.Add(a));
            mock_habitat_table.Setup(t => t.Remove(It.IsAny<Habitat>())).Callback((Habitat a) => habitatList.Remove(a));
            mock_habitat_table.Setup(t => t.RemoveRange(It.IsAny<IEnumerable<Habitat>>())).Callback((IEnumerable<Habitat> a) => habitatList.RemoveRange(0, a.Count<Habitat>()));

        }

        [TestInitialize]
        public void Initialize()
        {
            mock_context = new Mock<ZooContext>();
            mock_animal_table = new Mock<DbSet<Animal>>();
            mock_habitat_table = new Mock<DbSet<Habitat>>();
            habitatList = new List<Habitat>();
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
        public void RepoHasNoHabitats()
        {
            ConnectMocksToDatastore();

            List<Habitat> habitats = repo.GetHabitats();
            int expected_habitat_count = 0;
            int actual_habitat_count = habitats.Count;

            // Assert
            Assert.AreEqual(expected_habitat_count, actual_habitat_count);
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
        public void RepoCanAddHabitatToDatabase()
        {
            ConnectMocksToDatastore();
            Habitat newHabitat = new Habitat { HabitatId = 1, Name = "x", HabitatTypeId = 3};

            repo.AddHabitat(newHabitat);

            int actual_habitat_count = repo.GetHabitats().Count;
            int expected_habitat_count = 1;

            Assert.AreEqual(expected_habitat_count, actual_habitat_count);
        }
        [TestMethod]
        public void RepoCanFindAnimal()
        {
            animalList.Add(new Animal { AnimalId = 1, Name = "x", CommonName = "y", ScientificName = "z", HabitatId = 5, Age = 8 });
            animalList.Add(new Animal { AnimalId = 2, Name = "a", CommonName = "b", ScientificName = "c", HabitatId = 3, Age = 20 });
            animalList.Add(new Animal { AnimalId = 3, Name = "q", CommonName = "r", ScientificName = "s", HabitatId = 8, Age = 305 });
            ConnectMocksToDatastore();

            int animalId = 2;
            Animal actual_animal = repo.FindAnimal(animalId);

            int expected_animal_id = 2;
            int actual_animal_id = actual_animal.AnimalId;
            Assert.AreEqual(expected_animal_id, actual_animal_id);

        }
        [TestMethod]
        public void RepoCanFindHabitat()
        {
            habitatList.Add(new Habitat { HabitatId = 1, Name = "x", HabitatTypeId = 3 });
            habitatList.Add(new Habitat { HabitatId = 2, Name = "y", HabitatTypeId = 1 });
            habitatList.Add(new Habitat { HabitatId = 3, Name = "z", HabitatTypeId = 8 });
            ConnectMocksToDatastore();

            int habitatId = 2;
            Habitat actual_habitat = repo.FindHabitat(habitatId);

            int expected_habitat_id = 2;
            int actual_habitat_id = actual_habitat.HabitatId;
            Assert.AreEqual(expected_habitat_id, actual_habitat_id);
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
        [TestMethod]
        public void RepoCanGetAllHabitats()
        {
            habitatList.Add(new Habitat { HabitatId = 1, Name = "x", HabitatTypeId = 3 });
            habitatList.Add(new Habitat { HabitatId = 2, Name = "y", HabitatTypeId = 1 });
            habitatList.Add(new Habitat { HabitatId = 3, Name = "z", HabitatTypeId = 8 });
            ConnectMocksToDatastore();

            List<Habitat> expected_habitat_list = new List<Habitat>();
            expected_habitat_list.Add(new Habitat { HabitatId = 1, Name = "x", HabitatTypeId = 3 });
            expected_habitat_list.Add(new Habitat { HabitatId = 2, Name = "y", HabitatTypeId = 1 });
            expected_habitat_list.Add(new Habitat { HabitatId = 3, Name = "z", HabitatTypeId = 8 });

            List<Habitat> actual_habitat_list = repo.GetHabitats();

            Assert.AreEqual(expected_habitat_list, actual_habitat_list);

        }
    }
}
