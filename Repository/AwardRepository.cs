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

    public class AwardRepository : IAwardRepository
    {
        private readonly DapperContext _context;

        public AwardRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Award> CreateAward(AwardForCreationDto award)
        {
            var query = "INSERT INTO Awards (Name, Amount) VALUES (@Name, @Amount)" +
                        "SELECT CAST(SCOPE_IDENTITY() AS int)";
                

            var parameters = new DynamicParameters();
            parameters.Add("Name", award.Name, DbType.String);
            parameters.Add("Amount", award.Amount, DbType.Double);

            using(var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);

                var createdAward = new Award
                {
                    Id = id,
                    Name = award.Name,
                    Amount = (double)award.Amount
                };

                return createdAward;
    
            }
            
        }

        public async Task DeleteAward(int id)
        {
            var query = "DELETE  FROM Awards WHERE Id = @Id";

            using(var connection=_context.CreateConnection()) 
            {
                await connection.ExecuteAsync(query, new {id});
            }
        }

      

        public async Task<Award> GetAward(int id)
        {
            var query = "SELECT * FROM Awards WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var award = await connection.QuerySingleOrDefaultAsync<Award>(query, new { Id = id });
                return award;
            }
        }

        public async Task<IEnumerable<Award>> GetAwards()
        {
            var query = "SELECT Id, Name, Amount FROM Awards";

            using(var connection = _context.CreateConnection())
            {
                var awards = await connection.QueryAsync<Award>(query);
                return awards.ToList();
            }
        }

        public async Task UpdateAward(int id, AwardForUpdateDto award)
        {
            var checkQuery = "SELECT COUNT(1) FROM Awards WHERE Name = @Name AND Id <> @Id";
            var updateQuery = "UPDATE Awards SET Name = @Name, Amount = @Amount WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("Name", award.Name, DbType.String);
            parameters.Add("Amount", award.Amount, DbType.Double);

            using (var connection = _context.CreateConnection())
            {
                
                var count = await connection.ExecuteScalarAsync<int>(checkQuery, parameters);
                if (count > 0)
                {
                    throw new Exception("A record with the same name already exists.");
                }

                
                await connection.ExecuteAsync(updateQuery, parameters);
            }
        }
    }
}
