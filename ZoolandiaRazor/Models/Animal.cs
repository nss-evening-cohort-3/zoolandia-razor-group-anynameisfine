using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ZoolandiaRazor.Models
{

    public class Animal
    {
        [Key]
        public int AnimalId { get; set; }

        [Required]
        public string Name { get; set; }

        public string CommonName { get; set; }

        [Required]
        public string ScientificName { get; set; }

        [Required]
        public int HabitatId { get; set; }

        [Required]
        public int Age { get; set; }

    }
}