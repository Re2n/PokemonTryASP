using PokemonTryASP.Dto;
using PokemonTryASP.Entities;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using PokemonTryASP.Interface;

namespace PokemonTryASP.Repository;

public class PassportRepository : IPassportRepository
{
    private string connectionString;

    public PassportRepository(IConfiguration config)
    {
        connectionString = config.GetConnectionString("DefaultConnection");
    }

    public async Task<PassportDto> CreateAsync(PassportDto passport)
    {
        await using (var db = new NpgsqlConnection(connectionString))
        {
            await db.OpenAsync();
            var query =
                "INSERT INTO \"Passports\" (\"Type\", \"Number\", \"WorkerId\") VALUES (@Type, @Number, @WorkerId) RETURNING \"Type\", \"Number\", \"WorkerId\"";
            return await db.QueryFirstOrDefaultAsync<PassportDto>(query, passport);
        }
    }

    public async Task<bool> DeleteAsync(int PassportId)
    {
        await using (var db = new NpgsqlConnection(connectionString))
        {
            await db.OpenAsync();
            var query = "DELETE FROM \"Passports\" WHERE \"Id\" = @id";
            var res = await db.ExecuteAsync(query, new { id = PassportId });
            return true;
        }
    }

    public async Task<PassportDto> GetByIdAsync(int PassportId)
    {
        await using (var db = new NpgsqlConnection(connectionString))
        {
            await db.OpenAsync();
            var query = "SELECT \"Type\", \"Number\", \"WorkerId\" FROM \"Passports\" WHERE \"Id\" = @id";
            var par = new { id = PassportId };
            return await db.QueryFirstOrDefaultAsync<PassportDto>(query, par);
        }
    }

    public async Task<ICollection<PassportDto>> GetAllAsync()
    {
        await using (var db = new NpgsqlConnection(connectionString))
        {
            await db.OpenAsync();
            var query = "SELECT \"Type\", \"Number\", \"WorkerId\" FROM \"Passports\"";
            var passports = await db.QueryAsync<PassportDto>(query);
            return passports.ToList();
        }
    }

    public async Task<PassportDto> UpdateAsync(int PassportId, PassportDtoUpdate passport)
    {
        await using (var db = new NpgsqlConnection(connectionString))
        {
            await db.OpenAsync();
            var query =
                "UPDATE \"Passports\" SET \"Type\" = COALESCE(@Type, \"Type\"), \"Number\" = COALESCE(@Number, \"Number\"), \"WorkerId\" = COALESCE(@WorkerId, \"WorkerId\") WHERE \"Id\" = @Id RETURNING \"Type\", \"Number\", \"WorkerId\"";
            var par = new
            {
                Id = PassportId,
                Type = passport.Type,
                Number = passport.Number,
                WorkerId = passport.WorkerId
            };
            return await db.QueryFirstOrDefaultAsync<PassportDto>(query, par);
        }
    }

    public async Task<PassportDto> GetByWorkerIdAsync(int WorkerId)
    {
        await using (var db = new NpgsqlConnection(connectionString))
        {
            await db.OpenAsync();
            var query = "SELECT \"Type\", \"Number\", \"WorkerId\" FROM \"Passports\" WHERE \"WorkerId\" = @Workerid";
            var par = new { Workerid = WorkerId };
            return await db.QueryFirstOrDefaultAsync<PassportDto>(query, par);
        }
    }
}