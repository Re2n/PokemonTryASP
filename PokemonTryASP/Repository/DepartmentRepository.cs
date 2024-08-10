using PokemonTryASP.Dto;
using PokemonTryASP.Entities;
using Dapper;
using Npgsql;
using PokemonTryASP.Interface;

namespace PokemonTryASP.Repository;

public class DepartmentRepository : IDepartmentRepository
{
        private string connectionString;

    public DepartmentRepository(IConfiguration config)
    {
        connectionString = config.GetConnectionString("DefaultConnection");
    }
    
    public async Task<DepartmentDto> CreateAsync(DepartmentDto department)
    {
        await using (var db = new NpgsqlConnection(connectionString))
        {
            await db.OpenAsync();
            var query = "INSERT INTO \"Departments\" (\"Name\", \"Phone\", \"CompanyId\") VALUES (@Name, @Phone, @CompanyId) RETURNING \"Name\", \"Phone\", \"CompanyId\"";
            return await db.QueryFirstOrDefaultAsync<DepartmentDto>(query, department);
        }
    }
    
    public async Task<bool> DeleteAsync(int DepartmentId)
    {
        await using (var db = new NpgsqlConnection(connectionString))
        {
            await db.OpenAsync();
            var query = "DELETE FROM \"Departments\" WHERE \"Id\" = @id";
            var res = await db.ExecuteAsync(query, new { id = DepartmentId });
            return true;
        }
    }
    
    public async Task<DepartmentDto> GetByIdAsync(int DepartmentId)
    {
        await using (var db = new NpgsqlConnection(connectionString))
        {
            await db.OpenAsync();
            var query = "SELECT \"Name\", \"Phone\", \"CompanyId\" FROM \"Departments\" WHERE \"Id\" = @id";
            var par = new { id = DepartmentId };
            return await db.QueryFirstOrDefaultAsync<DepartmentDto>(query, par);
        }
    }
    
    public async Task<ICollection<DepartmentDto>> GetAllAsync()
    {
        await using (var db = new NpgsqlConnection(connectionString))
        {
            await db.OpenAsync();
            var query = "SELECT \"Name\", \"Phone\", \"CompanyId\" FROM \"Departments\"";
            var departments = await db.QueryAsync<DepartmentDto>(query);
            return departments.ToList();
        }
    }
    
    public async Task<DepartmentDto> UpdateAsync(int DepartmentId, DepartmentDtoUpdate department)
    {
        await using (var db = new NpgsqlConnection(connectionString))
        {
            await db.OpenAsync();
            var query =
                "UPDATE \"Departments\" SET \"Name\" = COALESCE(@Name, \"Name\"), \"Phone\" = COALESCE(@Phone, \"Phone\"), \"CompanyId\" = COALESCE(@CompanyId, \"CompanyId\") WHERE \"Id\" = @Id RETURNING \"Name\", \"Phone\", \"CompanyId\"";
            var par = new
            {
                Id = DepartmentId,
                Name = department.Name,
                Phone = department.Phone,
                CompanyId = department.CompanyId
            };
            return await db.QueryFirstOrDefaultAsync<DepartmentDto>(query, par);
        }
    }
}