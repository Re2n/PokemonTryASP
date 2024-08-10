using PokemonTryASP.Dto;
using PokemonTryASP.Entities;
using PokemonTryASP.Interface;
using Microsoft.AspNetCore.Mvc;

namespace PokemonTryASP.Controllers;


[Route("api/[controller]")]
[ApiController]

public class CompanyController : Controller
{
    private readonly ICompanyRepository _companyRepository;

    public CompanyController(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }
    
    [HttpPost("CreateCompany")]
    public async Task<IActionResult> CreateCompanyAsync(CompanyDto newCompany)
    {
        if (newCompany == null)
        {
            return BadRequest(ModelState);
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var companyDb = new CompanyDto
        {
            Name = newCompany.Name
        };
        await _companyRepository.CreateAsync(companyDb);
        return Ok(companyDb);
    }
    
    [HttpGet("GetAllCompanies")]
    public async Task<IActionResult> GetAllCompaniesAsync()
    {
        var companies = await _companyRepository.GetAllAsync();
        return Ok(companies);
    }
    
    [HttpGet("GetCompanyById/{CompanyId}")]
    public async Task<IActionResult> GetCompanyByIdAsync([FromRoute] int CompanyId)
    {
        var company = await _companyRepository.GetByIdAsync(CompanyId);
        return Ok(company);
    }
    
    [HttpDelete("DeleteCompany/{CompanyId}")]
    public async Task<IActionResult> DeleteCompanyAsync([FromRoute] int CompanyId)
    {
        return Ok(await _companyRepository.DeleteAsync(CompanyId));
    }
    
    [HttpPatch("UpdateCompany/{CompanyId}")]
    public async Task<IActionResult> UpdateCompany([FromRoute] int CompanyId, CompanyDtoUpdate company)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(await _companyRepository.UpdateAsync(CompanyId, company));
    }
}