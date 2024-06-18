using PokemonTryASP.Dto;
using PokemonTryASP.Entities;
using PokemonTryASP.Interface;
using Microsoft.AspNetCore.Mvc;

namespace PokemonTryASP.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkerController : Controller
{
    private readonly IWorkerRepository _workerRepository;

    public WorkerController(IWorkerRepository workerRepository)
    {
        _workerRepository = workerRepository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateWorkerAsync(WorkerDto newWorker)
    {
        if (newWorker == null)
        {
            return BadRequest(ModelState);
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var workerDb = new Worker
        {
            Name = newWorker.Name,
            Surname = newWorker.Surname,
            Phone = newWorker.Phone,
            CompanyId = newWorker.CompanyId,
            Bobik = newWorker.Bobik
        };
        await _workerRepository.CreateAsync(workerDb);
        return Ok(workerDb);
    }
}