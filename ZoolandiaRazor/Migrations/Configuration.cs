namespace ZoolandiaRazor.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ZoolandiaRazor.DAL.ZooContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ZoolandiaRazor.DAL.ZooContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var Forest = new Habitat { Name = "Forest", HabitatTypeId = 1 };
            var Jungle = new Habitat { Name = "Jungle", HabitatTypeId = 2 };
            var Plains = new Habitat { Name = "Plains", HabitatTypeId = 3 };
            var Ocean = new Habitat { Name = "Ocean", HabitatTypeId = 4 };
            context.Habitats.AddOrUpdate(
                p => p.Name, Plains, Ocean, Jungle, Forest
            );
            var Bulbasaur = new Animal { Name = "Bulbasaur", CommonName = "Ivysaur", ScientificName = "Venusaur", Age = 1, Habitat = Jungle };
            var Charmander = new Animal { Name = "Charmander", CommonName = "Charmeleon", ScientificName = "Charizard", Age = 2, Habitat = Forest };
            var Squirtle = new Animal { Name = "Squirtle", CommonName = "Wartortle", ScientificName = "Blastiose", Age = 3, Habitat = Ocean };
            context.Animals.AddOrUpdate(
                p => p.Name, Bulbasaur, Charmander, Squirtle
            );
        }
    }
}
