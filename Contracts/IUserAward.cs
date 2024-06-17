using EvonaZadatak.Dto;
using EvonaZadatak.Models;

namespace EvonaZadatak.Contracts
{
    public interface IUserAward
    {
        public Task<AwardUser> CreateUserAward(UserAwardForCreationDto userAward);
        public Task<List<UserAwardDto>> SearchUserAwards(int userId,DateTime searchDate);
        public Task AddAwardToAllUsers(int awardId);

    }
}
