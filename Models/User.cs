using System.ComponentModel.DataAnnotations;
using System;

namespace EvonaZadatak.Models
{
    public class User
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string? LastName { get; set; }

        [Required]
        [Range(1000000000000, 9999999999999, ErrorMessage = "JMBG must be exactly 13 digits long.")]
        public long JMBG { get; set; }


        [Required]
        public DateTime RegistrationDate{ get; set; }
    }
}
