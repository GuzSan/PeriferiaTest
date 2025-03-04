using EmployeeProyect.Domain.Models;

namespace EmployeeProyect.Application.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<List<DepartmentModel>> GetAllAsync();
        Task<DepartmentModel?> GetByIdAsync(int id);
        Task AddAsync(DepartmentModel Department);
        Task UpdateAsync(DepartmentModel Department);
        Task DeleteAsync(int id);
    }
}
