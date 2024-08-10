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

    [HttpPost("CreateWorker")]
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
        
        var workerDb = new WorkerDto
        {
            Name = newWorker.Name,
            Surname = newWorker.Surname,
            Phone = newWorker.Phone,
            CompanyId = newWorker.CompanyId,
            DepartmentId = newWorker.DepartmentId
        };
        await _workerRepository.CreateAsync(workerDb);
        return Ok(workerDb);
    }

    [HttpGet("GetAllWorkers")]
    public async Task<IActionResult> GetAllWorkersAsync()
    {
        var workers = await _workerRepository.GetAllAsync();
        return Ok(workers);
    }

    [HttpGet("GetWorkerById/{WorkerId}")]
    public async Task<IActionResult> GetWorkerByIdAsync([FromRoute] int WorkerId)
    {
        var worker = await _workerRepository.GetByIdAsync(WorkerId);
        return Ok(worker);
    }

    [HttpDelete("DeleteWorker/{WorkerId}")]
    public async Task<IActionResult> DeleteWorkerAsync([FromRoute] int WorkerId)
    {
        return Ok(await _workerRepository.DeleteAsync(WorkerId));
    }

    [HttpGet("GetByCompanyId/{CompanyId}")]
    public async Task<IActionResult> GetByCompanyId([FromRoute] int CompanyId)
    {
        var workers = await _workerRepository.GetByCompanyId(CompanyId);
        return Ok(workers);
    }

    [HttpGet("GetByDepartmentId/{CompanyId}, {DepartmentId}")]
    public async Task<IActionResult> GetByDepartmentId([FromRoute] int CompanyId, [FromRoute] int DepartmentId)
    {
        var workers = await _workerRepository.GetByDepartmentId(CompanyId, DepartmentId);
        return Ok(workers);
    }

    [HttpPatch("WorkerUpdate/{WorkerId}")]
    public async Task<IActionResult> UpdateWorker([FromRoute] int WorkerId, WorkerDtoUpdate worker)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(await _workerRepository.UpdateAsync(WorkerId, worker));
    }
}