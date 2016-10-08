using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZoolandiaRazor.Models
{
    public class Habitat
    {
        [Key]
        public int HabitatId { get; set; }

        [Required]
        public string HabitatName { get; set; }

        [Required]
        public int HabitatTypeId { get; set; }

        public List<Animal> Residents { get; set; }

        //public List<Employee> Employees { get; set; }

    }
}