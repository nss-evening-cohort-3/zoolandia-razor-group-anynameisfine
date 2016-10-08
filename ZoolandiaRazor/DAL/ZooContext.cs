using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ZoolandiaRazor.Models;

namespace ZoolandiaRazor.DAL
{
    public class ZooContext : DbContext
    {
        public virtual DbSet<Animal> Animals { get; set; }
        public virtual DbSet<Habitat> Habitats { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
    }
}