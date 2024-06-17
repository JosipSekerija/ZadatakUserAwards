using EvonaZadatak.Dto;
using EvonaZadatak.Models;
using Microsoft.AspNetCore.Mvc;

namespace EvonaZadatak.Contracts
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> GetUsers();

        public Task<User> GetUser(int id);

        public Task<User> CreateUser(UserForCreationDto user);

        public Task UpdateUser(int id, UserForUpdateDto user);
        public Task DeleteUser(int id);
    }
}
