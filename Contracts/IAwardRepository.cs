using EvonaZadatak.Dto;
using EvonaZadatak.Models;
using Microsoft.AspNetCore.Mvc;

namespace EvonaZadatak.Contracts
{
    public interface IAwardRepository
    {
        public Task<IEnumerable<Award>> GetAwards();
        public Task <Award> GetAward(int id);

        public Task<Award> CreateAward(AwardForCreationDto award);

        public Task UpdateAward(int id,AwardForUpdateDto award);
        public Task DeleteAward(int id);

       
    }
}
