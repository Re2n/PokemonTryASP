using PokemonTryASP.Interface;
using PokemonTryASP.Entities;
using Dapper;
using Npgsql;
using PokemonTryASP.Dto;

namespace PokemonTryASP.Repository;

public class WorkerRepository : IWorkerRepository
{
    private string connectionString;

    public WorkerRepository(IConfiguration config)
    {
        connectionString = config.GetConnectionString("DefaultConnection");
    }

    public async Task<WorkerDto> CreateAsync(WorkerDto worker)
    {
        await using (var db = new NpgsqlConnection(connectionString))
        {
            await db.OpenAsync();
            var query = "INSERT INTO \"Workers\" (\"Name\", \"Surname\", \"Phone\", \"CompanyId\", \"DepartmentId\") VALUES (@Name, @Surname, @Phone, @CompanyId, @DepartmentId) RETURNING *";
            return await db.QueryFirstOrDefaultAsync<WorkerDto>(query, worker);
        }
    }

    public async Task<bool> DeleteAsync(int WorkerId)
    {
        await using (var db = new NpgsqlConnection(connectionString))
        {
            await db.OpenAsync();
            var query = "DELETE FROM \"Workers\" WHERE \"Id\" = @id";
            var res = await db.ExecuteAsync(query, new { id = WorkerId });
            return true;
        }
    }

    public async Task<WorkerDto> GetByIdAsync(int WorkerId)
    {
        await using (var db = new NpgsqlConnection(connectionString))
        {
            await db.OpenAsync();
            var query = "SELECT \"Name\", \"Surname\", \"Phone\", \"CompanyId\", \"DepartmentId\" FROM \"Workers\" WHERE \"Id\" = @id";
            var par = new { id = WorkerId };
            return await db.QueryFirstOrDefaultAsync<WorkerDto>(query, par);
        }
    }

    public async Task<ICollection<WorkerDto>> GetAllAsync()
    {
        await using (var db = new NpgsqlConnection(connectionString))
        {
            await db.OpenAsync();
            var query = "SELECT \"Name\", \"Surname\", \"Phone\", \"CompanyId\", \"DepartmentId\" FROM \"Workers\"";
            var workers = await db.QueryAsync<WorkerDto>(query);
            return workers.ToList();
        }
    }

    public async Task<ICollection<WorkerDto>> GetByCompanyId(int CompanyId)
    {
        await using (var db = new NpgsqlConnection(connectionString))
        {
            await db.OpenAsync();
            var query = "SELECT \"Name\", \"Surname\", \"Phone\", \"CompanyId\", \"DepartmentId\" FROM \"Workers\" WHERE \"CompanyId\" = @companyId";
            var par = new { companyId = CompanyId };
            var workers = await db.QueryAsync<WorkerDto>(query, par);
            return workers.ToList();
        }
    }

    public async Task<ICollection<WorkerDto>> GetByDepartmentId(int CompanyId, int DepartmentId)
    {
        await using (var db = new NpgsqlConnection(connectionString))
        {
            await db.OpenAsync();
            var query =
                "SELECT \"Name\", \"Surname\", \"Phone\", \"CompanyId\", \"DepartmentId\" FROM \"Workers\" WHERE \"CompanyId\" = @companyId AND \"DepartmentId\" = @departmentId";
            var par = new { companyId = CompanyId, departmentId = DepartmentId };
            var workers = await db.QueryAsync<WorkerDto>(query, par);
            return workers.ToList();
        }
    }

    public async Task<WorkerDto> UpdateAsync(int WorkerId, WorkerDtoUpdate worker)
    {
        await using (var db = new NpgsqlConnection(connectionString))
        {
            await db.OpenAsync();
            var query =
                "UPDATE \"Workers\" SET \"Name\" = COALESCE(@Name, \"Name\"), \"Surname\" = COALESCE(@Surname, \"Surname\"), \"Phone\" = COALESCE(@Phone, \"Phone\"), \"CompanyId\" = COALESCE(@CompanyId, \"CompanyId\"), \"DepartmentId\" = COALESCE(@DepartmentId, \"DepartmentId\") WHERE \"Id\" = @Id RETURNING *";
            var par = new
            {
                Id = WorkerId,
                Name = worker.Name,
                Surname = worker.Surname,
                Phone = worker.Phone,
                CompanyId = worker.CompanyId,
                DepartmentId = worker.DepartmentId
            };
            return await db.QueryFirstOrDefaultAsync<WorkerDto>(query, par);
        }
    }
}