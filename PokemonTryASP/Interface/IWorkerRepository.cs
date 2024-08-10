using PokemonTryASP.Entities;
using PokemonTryASP.Dto;
namespace PokemonTryASP.Interface;

public interface IWorkerRepository
{
    public Task<WorkerDto> CreateAsync(WorkerDto worker);
    public Task<bool> DeleteAsync(int workerId);
    public Task<WorkerDto> GetByIdAsync(int workerId);
    public Task<ICollection<WorkerDto>> GetAllAsync();
    public Task<ICollection<WorkerDto>> GetByCompanyId(int companyId);
    public Task<ICollection<WorkerDto>> GetByDepartmentId(int companyId, int departmentId);
    public Task<WorkerDto> UpdateAsync(int workerId, WorkerDtoUpdate worker);
}