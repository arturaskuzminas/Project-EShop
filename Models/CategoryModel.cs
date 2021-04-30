using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.Models
{
    public class CategoryModel
    {
        [Key]
        [Display(Name = "ID")]
        [Required(ErrorMessage = "Laukas 'ID' yra privalomas")]
        public int ID { get; set; }

        [Display(Name = "Pavadinimas")]
        [Required(ErrorMessage = "Laukas 'Pavadinimas' yra privalomas")]
        public string Title { get; set; }

        public int? ParentID { get; set; }

        [ForeignKey("ParentID")]
        public CategoryModel Parent { get; set; }
    }
}
