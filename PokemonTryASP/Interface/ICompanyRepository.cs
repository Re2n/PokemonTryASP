using PokemonTryASP.Dto;
using PokemonTryASP.Entities;

namespace PokemonTryASP.Interface;

public interface ICompanyRepository
{
    public Task<CompanyDto> CreateAsync(CompanyDto company);
    public Task<bool> DeleteAsync(int companyId);
    public Task<CompanyDto> GetByIdAsync(int companyId);
    public Task<ICollection<CompanyDto>> GetAllAsync();
    public Task<CompanyDto> UpdateAsync(int companyId, CompanyDtoUpdate company);
}