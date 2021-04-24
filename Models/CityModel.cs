using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.Models
{
    public class CityModel
    {
        [Key]
        [Required(ErrorMessage = "Laukas 'Miesto kodas' yra privalomas")]
        [Display(Name = "Miesto kodas")]
        public string ID { get; set; }

        [Required(ErrorMessage = "Laukas 'Miesto pavadinimas' yra privalomas")]
        [Display(Name = "Miesto pavadinimas")]
        public string Name { get; set; }
    }
}
