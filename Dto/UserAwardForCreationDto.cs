namespace EvonaZadatak.Dto
{
    public class UserAwardForCreationDto
    {
        public int UserId { get; set; }
        public int AwardId { get; set; }

        public DateOnly DateAwarded { get; set; }
    }
}
