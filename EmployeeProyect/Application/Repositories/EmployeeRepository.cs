using EmployeeProyect.Application.Interfaces;
using EmployeeProyect.Application.Services;
using EmployeeProyect.Domain.Models;
using EmployeeProyect.Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EmployeeProyect.Application.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDBContext _context;
        

        public EmployeeRepository(EmployeeDBContext context)
        {
            _context = context;
            
        }

        public async Task<List<EmployeeModel>> GetAllAsync() => await _context.Employees.ToListAsync();
        public async Task<EmployeeModel?> GetByIdAsync(int id) => await _context.Employees.FindAsync(id);

        public async Task AddAsync(EmployeeModel employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(EmployeeModel employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }
    }
}
