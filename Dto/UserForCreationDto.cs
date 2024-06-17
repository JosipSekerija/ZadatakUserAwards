using System.ComponentModel.DataAnnotations;

namespace EvonaZadatak.Dto
{
    public class UserForCreationDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }


        [Range(1000000000000, 9999999999999, ErrorMessage = "JMBG must be exactly 13 digits long.")]
        public long JMBG { get; set; }

        public DateTime RegistrationDate { get; set; }
    }
}
