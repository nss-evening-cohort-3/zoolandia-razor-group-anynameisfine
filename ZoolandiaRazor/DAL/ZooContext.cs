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
    }
}