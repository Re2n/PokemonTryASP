using PokemonTryASP.Dto;
using PokemonTryASP.Entities;
using PokemonTryASP.Interface;
using Microsoft.AspNetCore.Mvc;

namespace PokemonTryASP.Controllers;

[Route("api/[controller]")]
[ApiController]

public class DepartmentController : Controller
{
    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentController(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }
    
    [HttpPost("CreateDepartment")]
    public async Task<IActionResult> CreateDepartmentAsync(DepartmentDto newDepartment)
    {
        if (newDepartment == null)
        {
            return BadRequest(ModelState);
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var departmentDb = new DepartmentDto
        {
            Name = newDepartment.Name,
            Phone = newDepartment.Phone,
            CompanyId = newDepartment.CompanyId
        };
        await _departmentRepository.CreateAsync(departmentDb);
        return Ok(departmentDb);
    }
    
    [HttpGet("GetAllDepartments")]
    public async Task<IActionResult> GetAllDepartmentsAsync()
    {
        var departments = await _departmentRepository.GetAllAsync();
        return Ok(departments);
    }
    
    [HttpGet("GetDepartmentById/{DepartmentId}")]
    public async Task<IActionResult> GetDepartmentByIdAsync([FromRoute] int DepartmentId)
    {
        var department = await _departmentRepository.GetByIdAsync(DepartmentId);
        return Ok(department);
    }
    
    [HttpDelete("DeleteDepartment/{DepartmentId}")]
    public async Task<IActionResult> DeleteDepartmentAsync([FromRoute] int DepartmentId)
    {
        return Ok(await _departmentRepository.DeleteAsync(DepartmentId));
    }
    
    [HttpPatch("UpdateDepartment/{DepartmentId}")]
    public async Task<IActionResult> UpdateDepartment([FromRoute] int DepartmentId, DepartmentDtoUpdate department)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(await _departmentRepository.UpdateAsync(DepartmentId, department));
    }
}