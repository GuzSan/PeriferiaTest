using EmployeeProyect.Application.Interfaces;
using EmployeeProyect.Domain.Enums;
using EmployeeProyect.Domain.Models;

namespace EmployeeProyect.Application.InMemoryRepositories
{
    public class InMemoryEmployee
    {
        private readonly List<EmployeeModel> _employees = new();

        public InMemoryEmployee()
        {
            _employees.AddRange(new[]
            {
            new EmployeeModel { Id = 1, Name = "Santiago", Email = "santiagog1061@gmail.com", BaseSalary = 5000, Position = JobPosition.Developer, DepartmentId = 1 },
            new EmployeeModel { Id = 2, Name = "Jose", Email = "jose@example.com", BaseSalary = 6000, Position = JobPosition.Manager, DepartmentId = 1 },
            new EmployeeModel { Id = 3, Name = "Charlie", Email = "charlie@example.com", BaseSalary = 4500, Position = JobPosition.HR, DepartmentId = 2 }
        });
        }

        public async Task<List<EmployeeModel>> GetAllAsync() => await Task.FromResult(_employees);
        public async Task<EmployeeModel?> GetByIdAsync(int id) => await Task.FromResult(_employees.FirstOrDefault(e => e.Id == id));
        public async Task AddAsync(EmployeeModel employee)
        {
            employee.Id = _employees.Count + 1;
            _employees.Add(employee);
            await Task.CompletedTask;
        }
        public async Task RemoveAsync(int id) {
            EmployeeModel? employee = _employees.Find(e => e.Id == id);
            if (employee != null) _employees.Remove(employee);
            await Task.CompletedTask;
        }
    }

}
