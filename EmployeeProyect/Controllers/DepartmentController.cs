using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EmployeeProyect.Infrastucture.Persistence;
using EmployeeProyect.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeProyect.API.Controllers;

[ApiController]
[Route("api/departments")]
[Authorize] // Protegido con autenticación JWT
public class DepartmentsController : ControllerBase
{
    private readonly EmployeeDBContext _context;

    public DepartmentsController(EmployeeDBContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var departments = await _context.Departments.ToListAsync();
        return Ok(departments);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var department = await _context.Departments
            .Include(d => d.Employees)
            .FirstOrDefaultAsync(d => d.Id == id);

        if (department == null) return NotFound();
        return Ok(department);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DepartmentDto dto)
    {
        var department = new DepartmentModel { Name = dto.Name };
        _context.Departments.Add(department);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = department.Id }, department);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] DepartmentDto dto)
    {
        var department = await _context.Departments.FindAsync(id);
        if (department == null) return NotFound();

        department.Name = dto.Name;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var department = await _context.Departments.FindAsync(id);
        if (department == null) return NotFound();

        _context.Departments.Remove(department);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet("{id}/salary")]
    public async Task<IActionResult> GetTotalSalary(int id)
    {
        var department = await _context.Departments
            .Include(d => d.Employees)
            .FirstOrDefaultAsync(d => d.Id == id);

        if (department == null) return NotFound();

        decimal totalSalary = department.Employees.Sum(e => e.CalculateSalary());
        return Ok(new { department.Name, totalSalary });
    }
}

// DTO para departamentos
public class DepartmentDto
{
    public string Name { get; set; } = string.Empty;
}
