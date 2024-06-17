using EvonaZadatak.Contracts;
using EvonaZadatak.Models;

namespace EvonaZadatak.Dto
{
    
    
        public class UserAwardSearchDto
        {
            public DateTime? SearchDate { get; set; }
       
        public List<UserAwardDto> UserAwards { get; set; }
        }
    
}
