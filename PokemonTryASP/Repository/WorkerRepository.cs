using PokemonTryASP.Interface;
using PokemonTryASP.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using Npgsql;

namespace PokemonTryASP.Repository;

public class WorkerRepository : IWorkerRepository
{
    private string connectionString;

    public WorkerRepository(IConfiguration config)
    {
        connectionString = config.GetConnectionString("DefaultConnection");
    }

    public async Task<Worker> CreateAsync(Worker worker)
    {
        await using (var db = new NpgsqlConnection(connectionString))
        {
            await db.OpenAsync();
            var query = "INSERT INTO \"Workers\" (\"Name\", \"Surname\", \"Phone\", \"CompanyId\", \"Bobik\") VALUES (@Name, @Surname, @Phone, @CompanyId, @Bobik) RETURNING *";
            return await db.QueryFirstOrDefaultAsync<Worker>(query, worker);
        }
    }
}