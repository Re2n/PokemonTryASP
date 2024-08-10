using PokemonTryASP.Dto;
using PokemonTryASP.Entities;
using Dapper;
using Npgsql;
using PokemonTryASP.Interface;

namespace PokemonTryASP.Repository;

public class CompanyRepository : ICompanyRepository
{
    private string connectionString;

    public CompanyRepository(IConfiguration config)
    {
        connectionString = config.GetConnectionString("DefaultConnection");
    }
    
    public async Task<CompanyDto> CreateAsync(CompanyDto company)
    {
        await using (var db = new NpgsqlConnection(connectionString))
        {
            await db.OpenAsync();
            var query = "INSERT INTO \"Companies\" (\"Name\") VALUES (@Name) RETURNING \"Name\"";
            return await db.QueryFirstOrDefaultAsync<CompanyDto>(query, company);
        }
    }
    
    public async Task<bool> DeleteAsync(int CompanyId)
    {
        await using (var db = new NpgsqlConnection(connectionString))
        {
            await db.OpenAsync();
            var query = "DELETE FROM \"Companies\" WHERE \"Id\" = @id";
            var res = await db.ExecuteAsync(query, new { id = CompanyId });
            return true;
        }
    }
    
    public async Task<CompanyDto> GetByIdAsync(int CompanyId)
    {
        await using (var db = new NpgsqlConnection(connectionString))
        {
            await db.OpenAsync();
            var query = "SELECT \"Name\" FROM \"Companies\" WHERE \"Id\" = @id";
            var par = new { id = CompanyId };
            return await db.QueryFirstOrDefaultAsync<CompanyDto>(query, par);
        }
    }
    
    public async Task<ICollection<CompanyDto>> GetAllAsync()
    {
        await using (var db = new NpgsqlConnection(connectionString))
        {
            await db.OpenAsync();
            var query = "SELECT \"Name\" FROM \"Companies\"";
            var companies = await db.QueryAsync<CompanyDto>(query);
            return companies.ToList();
        }
    }
    
    public async Task<CompanyDto> UpdateAsync(int CompanyId, CompanyDtoUpdate company)
    {
        await using (var db = new NpgsqlConnection(connectionString))
        {
            await db.OpenAsync();
            var query =
                "UPDATE \"Companies\" SET \"Name\" = COALESCE(@Name, \"Name\") WHERE \"Id\" = @Id RETURNING \"Name\"";
            var par = new
            {
                Id = CompanyId,
                Name = company.Name,
            };
            return await db.QueryFirstOrDefaultAsync<CompanyDto>(query, par);
        }
    }
}