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
    }
}
