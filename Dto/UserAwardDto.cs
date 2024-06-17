namespace EvonaZadatak.Dto
{
    public class UserAwardDto
    {
        public int Id { get; set; } // Assuming this is the AwardUser's ID
        public string Name { get; set; } // Award name
        public decimal Amount { get; set; } // Award amount
        public DateTime DateAwarded { get; set; } // Date awarded
    }
}
