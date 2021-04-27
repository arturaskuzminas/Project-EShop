using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.Models
{
    public class ProductModel
    {
        [Key]
        [Display(Name = "ID")]
        [Required(ErrorMessage = "Laukas 'ID' yra privalomas")]
        public int ID { get; set; }

        [Display(Name = "Pavadinimas")]
        [Required(ErrorMessage = "Laukas 'Pavadinimas' yra privalomas")]
        public string Title { get; set; }

        [Display(Name = "Kaina")]
        [Required(ErrorMessage = "Laukas 'Kaina' yra privalomas")]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }

        [Display(Name = "Brand'as")]
        public string Brand { get; set; }

        [Display(Name = "Dydis")]
        public string Size { get; set; }

        [Display(Name = "Spalva")]
        public string Color { get; set; }

        [Display(Name = "Likutis")]
        [Required(ErrorMessage = "Laukas 'Likutis' yra privalomas")]
        public int StockCount { get; set; }

        [Display(Name = "Nuotr. link'as (PC)")]
        [Required]
        public string PictureLink { get; set; }
    }
}
