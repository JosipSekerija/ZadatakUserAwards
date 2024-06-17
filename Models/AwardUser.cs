using System.ComponentModel.DataAnnotations;

namespace EvonaZadatak.Models
{
    public class AwardUser
    {
        [Key]
        public int Id { get; set; }
        public int User_id {  get; set; }
        public int Award_id { get; set; }
       
        public DateOnly DateAwarded { get; set; }
    }
}
