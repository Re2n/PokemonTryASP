using PokemonTryASP.Dto;
using PokemonTryASP.Entities;
using PokemonTryASP.Interface;
using Microsoft.AspNetCore.Mvc;

namespace PokemonTryASP.Controllers;

[Route("api/[controller]")]
[ApiController]

public class PassportController : Controller
{
    private readonly IPassportRepository _passportRepository;

    public PassportController(IPassportRepository passportRepository)
    {
        _passportRepository = passportRepository;
    }
    
    [HttpPost("CreatePassport")]
    public async Task<IActionResult> CreatePassportAsync(PassportDto newPassport)
    {
        if (newPassport == null)
        {
            return BadRequest(ModelState);
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var passportDb = new PassportDto
        {
            Type = newPassport.Type,
            Number = newPassport.Number,
            WorkerId = newPassport.WorkerId
        };
        await _passportRepository.CreateAsync(passportDb);
        return Ok(passportDb);
    }
    
    [HttpGet("GetAllPassports")]
    public async Task<IActionResult> GetAllPassportsAsync()
    {
        var passports = await _passportRepository.GetAllAsync();
        return Ok(passports);
    }
    
    [HttpGet("GetPassportById/{PassportId}")]
    public async Task<IActionResult> GetPassportByIdAsync([FromRoute] int PassportId)
    {
        var passport = await _passportRepository.GetByIdAsync(PassportId);
        return Ok(passport);
    }

    [HttpGet("GetPassportByWorkerId/{WorkerId}")]
    public async Task<IActionResult> GetPassportByWorkerId([FromRoute] int WorkerId)
    {
        var passport = await _passportRepository.GetByWorkerIdAsync(WorkerId);
        return Ok(passport);
    }
    
    [HttpDelete("DeletePassport/{PassportId}")]
    public async Task<IActionResult> DeletePassportAsync([FromRoute] int PassportId)
    {
        return Ok(await _passportRepository.DeleteAsync(PassportId));
    }
    
    [HttpPatch("UpdatePassport/{PassportId}")]
    public async Task<IActionResult> UpdatePassport([FromRoute] int PassportId, PassportDtoUpdate passport)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(await _passportRepository.UpdateAsync(PassportId, passport));
    }
}