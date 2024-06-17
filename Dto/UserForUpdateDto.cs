using System.ComponentModel.DataAnnotations;

namespace EvonaZadatak.Dto
{
    public class UserForUpdateDto
    {

        public string? FirstName { get; set; }
        public string? LastName { get; set; }


        [Required]
        [Range(1000000000000, 9999999999999, ErrorMessage = "JMBG must be exactly 13 digits.")]
        public long JMBG { get; set; }
        
    }
}
