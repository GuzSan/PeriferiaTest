using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EmployeeProyect.Application.Interfaces;
using EmployeeProyect.Domain.Models;
using EmployeeProyect.Domain.Enums;
using EmployeeProyect.Application.Repositories;

namespace EmployeeProyect.Controllers;

[ApiController]
[Route("api/employees")]
[Authorize] // Requiere autenticación
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeRepository _repository;

    public EmployeesController(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var employees = await _repository.GetAllAsync();
        return Ok(employees);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var employee = await _repository.GetByIdAsync(id);
        if (employee == null) return NotFound();

        return Ok(employee);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] EmployeeDto dto)
    {
        var employee = new EmployeeModel
        {
            Name = dto.Name,
            Email = dto.Email,
            BaseSalary = dto.BaseSalary,
            Position = dto.Position,
            DepartmentId = dto.DepartmentId
        };

        await _repository.AddAsync(employee);
        return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] EmployeeDto dto)
    {
        var employee = await _repository.GetByIdAsync(id);
        if (employee == null) return NotFound();

        employee.Name = dto.Name;
        employee.Email = dto.Email;
        employee.BaseSalary = dto.BaseSalary;
        employee.Position = dto.Position;
        employee.DepartmentId = dto.DepartmentId;

        await _repository.UpdateAsync(employee);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}

// DTO para transferencia de datos
public class EmployeeDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public decimal BaseSalary { get; set; }
    public JobPosition Position { get; set; }
    public int DepartmentId { get; set; }
}
