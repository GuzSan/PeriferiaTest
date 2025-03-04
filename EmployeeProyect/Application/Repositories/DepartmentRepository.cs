
using EmployeeProyect.Application.Interfaces;
using EmployeeProyect.Domain.Models;

namespace EmployeeProyect.Application.Services
{
    public class DepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<IEnumerable<DepartmentModel>> GetAllDepartmentsAsync()
        {
            return await _departmentRepository.GetAllAsync();
        }

        public async Task<DepartmentModel> GetDepartmentByIdAsync(int id)
        {
            return await _departmentRepository.GetByIdAsync(id);
        }

        public async Task AddDepartmentAsync(DepartmentModel department)
        {
            await _departmentRepository.AddAsync(department);
        }

        public async Task UpdateDepartmentAsync(DepartmentModel department)
        {
            await _departmentRepository.UpdateAsync(department);
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            await _departmentRepository.DeleteAsync(id);
        }


    }
}
