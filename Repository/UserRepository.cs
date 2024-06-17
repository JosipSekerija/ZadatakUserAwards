using Dapper;
using EvonaZadatak.Contracts;
using EvonaZadatak.Data;
using EvonaZadatak.Dto;
using EvonaZadatak.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;

namespace EvonaZadatak.Repository
{
    public class UserRepository : IUserRepository
    {

        private readonly DapperContext _context;

        public UserRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<User> CreateUser(UserForCreationDto user)
        {
            var query ="INSERT INTO Users (FirstName, LastName, JMBG, RegistrationDate) VALUES (@FirstName, @LastName, @JMBG, @RegistrationDate)" +
                "SELECT CAST(SCOPE_IDENTITY() AS int)";


            var parameters = new DynamicParameters();
            parameters.Add("FirstName", user.FirstName, DbType.String);
            parameters.Add("LastName", user.LastName, DbType.String);
            parameters.Add("JMBG", user.JMBG, DbType.Int64);
            parameters.Add("RegistrationDate", DateTime.Now, DbType.Date);


            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);

                var createdUser = new User
                {
                    Id = id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    JMBG = (long)user.JMBG,
                    RegistrationDate =DateTime.Now
                };

                return createdUser;
            }
        }

        public async Task DeleteUser(int id)
        {
            var query = "DELETE  FROM Users WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }

        public async Task<User> GetUser(int id)
        {
            var query = "SELECT * FROM Users WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QuerySingleOrDefaultAsync<User>(query, new { Id = id });
                return user;
            }
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var query = "SELECT Id, FirstName, LastName, JMBG, RegistrationDate FROM Users";

            using (var connection = _context.CreateConnection())
            {
                var users = await connection.QueryAsync<User>(query);
                return users.ToList();
            }
        }

        public async Task UpdateUser(int id, UserForUpdateDto user)
        {
            var checkQuery = "SELECT COUNT(1) FROM Users WHERE JMBG = @JMBG AND Id <> @Id";
            var updateQuery = "UPDATE Users SET FirstName = @FirstName, LastName = @LastName, JMBG = @JMBG WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("FirstName", user.FirstName, DbType.String);
            parameters.Add("LastName", user.LastName, DbType.String);
            parameters.Add("JMBG", user.JMBG, DbType.Int64);

            using (var connection = _context.CreateConnection())
            {
                
                var count = await connection.ExecuteScalarAsync<int>(checkQuery, parameters);
                if (count > 0)
                {
                    throw new Exception("A record with the same name already exists.");
                }

                // Perform the update
                await connection.ExecuteAsync(updateQuery, parameters);
            }
        }
    }
}
