using System.ComponentModel.DataAnnotations;

namespace EvonaZadatak.Models
{
    public class Award
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public  string? Name { get; set; }
        [Required]
        public double Amount { get; set; }
        
    }
}
