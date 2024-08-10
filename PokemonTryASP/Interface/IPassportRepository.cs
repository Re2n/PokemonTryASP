using PokemonTryASP.Dto;

namespace PokemonTryASP.Interface;

public interface IPassportRepository
{
    public Task<PassportDto> CreateAsync(PassportDto passport);
    public Task<bool> DeleteAsync(int passportId);
    public Task<PassportDto> GetByIdAsync(int passportId);
    public Task<ICollection<PassportDto>> GetAllAsync();
    public Task<PassportDto> UpdateAsync(int passportId, PassportDtoUpdate passport);
    public Task<PassportDto> GetByWorkerIdAsync(int workerId);
}