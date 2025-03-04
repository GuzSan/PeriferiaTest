using EmployeeProyect.Application.InMemoryRepositories;
using EmployeeProyect.Application.Interfaces;
using EmployeeProyect.Application.Services;
using EmployeeProyect.Domain.Models;
using EmployeeProyect.Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EmployeeProyect.Application.Repositories
{
    public class EmployeeMemoryRepository : IEmployeeRepository
    {
        private readonly InMemoryEmployee _context;
        
        public EmployeeMemoryRepository(InMemoryEmployee context)
        {
            _context = context;
            
        }

        public async Task<List<EmployeeModel>> GetAllAsync() => await _context.GetAllAsync();
        public async Task<EmployeeModel?> GetByIdAsync(int id) => await _context.GetByIdAsync(id);

        public async Task AddAsync(EmployeeModel employee)
        {
            await _context.AddAsync(employee);
        }

        public Task UpdateAsync(EmployeeModel employee)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            await _context.RemoveAsync(id);
        }

        /* public async Task UpdateAsync(EmployeeModel employee)
        {
            _context.Employees.Update(employee);
            await _context();
        }*/

        /* public async Task DeleteAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
        } */
    }
}
