using Microsoft.AspNetCore.Mvc.Rendering;

namespace EvonaZadatak.Models
{
    public class UserAwardViewModel
    {
       
            public int UserId { get; set; }
            public string? UserName { get; set; }
        public int SelectedAwardId { get; set; }
        public List<SelectListItem>? Awards { get; set; }
        
    }
}
