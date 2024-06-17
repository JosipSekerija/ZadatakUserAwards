using Dapper;
using EvonaZadatak.Contracts;
using EvonaZadatak.Data;
using EvonaZadatak.Dto;
using EvonaZadatak.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace EvonaZadatak.Repository
{
    public class UserAwardRepository : IUserAward
    {
        private readonly DapperContext _context;

        public UserAwardRepository(DapperContext context)
        {
            _context = context;
        }
        public Task<AwardUser> CreateUserAward(UserAwardForCreationDto userAward)
        {
            var query = @"
    INSERT INTO Awards_Users (User_id, Award_id, DateAwarded)
    VALUES (@UserId, @AwardId, @DateAwarded);
    SELECT CAST(SCOPE_IDENTITY() as int);";

            var parameters = new DynamicParameters();
            parameters.Add("@UserId", userAward.UserId,DbType.Int32);
            parameters.Add("@AwardId", userAward.AwardId,DbType.Int32);
            parameters.Add("@DateAwarded", DateTime.Now, DbType.DateTime);

            using (var connection = _context.CreateConnection())
            {
                var id = connection.QuerySingle<int>(query, parameters);

                var createdUserAward = new AwardUser
                {
                    Id = id,
                    User_id = userAward.UserId,
                    Award_id = userAward.AwardId,
                    DateAwarded = DateOnly.FromDateTime(DateTime.Now)
                };

                return Task.FromResult(createdUserAward);
            }


            
        }

        public async Task<List<UserAwardDto>> SearchUserAwards(int userId, DateTime searchDate)
        {
            var query = @"
             SELECT au.Id, a.Name, a.Amount, au.DateAwarded
             FROM Awards_Users au
             INNER JOIN Awards a ON au.Award_id = a.Id
             WHERE au.User_id = @UserId AND CAST(au.DateAwarded AS DATE) = @SearchDate";

            Console.WriteLine($"Searching awards for userId: {userId}");
            using (var connection = _context.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserId", userId, DbType.Int32);
                parameters.Add("@SearchDate", searchDate, DbType.Date); // Ensure the DbType matches your column type

                var result = await connection.QueryAsync<UserAwardDto>(query, parameters);

                return result.ToList();
            }

        }



        public async Task<List<int>> GetAllUserIds()
        {
            var query = "SELECT Id FROM Users;"; // Adjust the table and column names as necessary

            using (var connection = _context.CreateConnection())
            {
                var userIds = await connection.QueryAsync<int>(query);
                return userIds.ToList();
            }
        }
        public async Task AddAwardToAllUsers(int awardId)
        {
            // Assuming you have a method to get all user IDs
            var userIds = await GetAllUserIds();

            foreach (var userId in userIds)
            {
                await CreateUserAward(new UserAwardForCreationDto
                {
                    UserId = userId,
                    AwardId = awardId
                });
            }
        }
    }
}
