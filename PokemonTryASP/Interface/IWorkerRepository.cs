using PokemonTryASP.Entities;
namespace PokemonTryASP.Interface;

public interface IWorkerRepository
{
    public Task<Worker> CreateAsync(Worker worker);
    /*public Task<Worker> DeleteAsync(int workerId);
    public Task<Worker> GetByIdAsync(int workerId);
    public Task<ICollection<Worker>> GetAllAsync();*/
}