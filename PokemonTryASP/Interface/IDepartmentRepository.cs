using PokemonTryASP.Dto;

namespace PokemonTryASP.Interface;

public interface IDepartmentRepository
{
    public Task<DepartmentDto> CreateAsync(DepartmentDto department);
    public Task<bool> DeleteAsync(int departmentId);
    public Task<DepartmentDto> GetByIdAsync(int departmentId);
    public Task<ICollection<DepartmentDto>> GetAllAsync();
    public Task<DepartmentDto> UpdateAsync(int departmentId, DepartmentDtoUpdate department);
}