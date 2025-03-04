
using EmployeeProyect.Application.Interfaces;
using EmployeeProyect.Domain.Models;

namespace EmployeeProyect.Application.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<EmployeeModel>> GetAllEmployeesAsync()
        {
            return await _employeeRepository.GetAllAsync();
        }

        public async Task AddEmployeeAsync(EmployeeModel employee)
        {
            employee.CalculateSalary();
            await _employeeRepository.AddAsync(employee);
        }
    }
}
