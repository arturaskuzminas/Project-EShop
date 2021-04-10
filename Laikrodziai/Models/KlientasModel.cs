using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Laikrodziai.Models
{
    public class KlientasModel
    {
        [DisplayName("ID")]
        [Required]
        public int ID { get; set; }

        [DisplayName("Vardas")]
        [Required]
        public string Vardas { get; set; }

        [DisplayName("Pavardė")]
        [Required]
        public string Pavarde { get; set; }

        [DisplayName("Adresas")]
        [Required]
        public string Adresas { get; set; }

        [DisplayName("E-paštas")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [DisplayName("Telefono Nr.")]
        [Required]
        public long? TelNr { get; set; }

        [DisplayName("Slaptažodis")]
        [Required]
        public string Slaptazodis { get; set; }

        [DisplayName("Registracijos data")]
        [Required]
        public DateTime RegData { get; set; }

        [DisplayName("Slaptažodis")]
        [Required]
        public string Miestas { get; set; }

        public override string ToString()
        {
            string line = $"{ID} {Vardas} {Pavarde} {Adresas} {Miestas} {Email} {TelNr} {Slaptazodis} {RegData}";
            return line;
        }
    }
}
