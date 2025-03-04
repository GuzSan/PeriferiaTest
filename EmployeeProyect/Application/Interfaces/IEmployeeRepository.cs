using EmployeeProyect.Domain.Models;

namespace EmployeeProyect.Application.Interfaces
{
    public interface IEmployeeRepository
    {

        Task<List<EmployeeModel>> GetAllAsync();
        Task<EmployeeModel?> GetByIdAsync(int id);
        Task AddAsync(EmployeeModel employee);
        Task UpdateAsync(EmployeeModel employee);
        Task DeleteAsync(int id);
    }
}
