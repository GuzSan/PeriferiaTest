using EmployeeProyect.Application.Services;
using EmployeeProyect.Application.Interfaces;
using EmployeeProyect.Domain.Enums;
using EmployeeProyect.Domain.Models;
using Moq;


namespace EmployeeProyect.TestAPI
{
    public class EmployeeServiceTests
    {
        private readonly Mock<IEmployeeRepository> _mockEmployeeRepository;
        private readonly EmployeeService _employeeService;

        public EmployeeServiceTests()
        {
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();
            _employeeService = new EmployeeService(_mockEmployeeRepository.Object);
        }

        [Fact]
        public async Task GetAllEmployeesAsync_ShouldReturnAllEmployees()
        {
            // Arrange
            var employees = new List<EmployeeModel>
            {
                new EmployeeModel { Id = 1, Name = "Santiago Guzman", Position = JobPosition.Developer, BaseSalary = 5000 },
                new EmployeeModel { Id = 2, Name = "Karina Grisales", Position = JobPosition.Manager, BaseSalary = 7000 }
            };

            _mockEmployeeRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(employees);

            // Act
            var result = await _employeeService.GetAllEmployeesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task AddEmployeeAsync_ShouldCalculateSalaryAndSaveEmployee()
        {
            // Arrange
            var employee = new EmployeeModel { Id = 1, Name = "Tatiana", Position = JobPosition.Developer, BaseSalary = 5000 };

            // Act
            await _employeeService.AddEmployeeAsync(employee);

            // Assert
            Assert.Equal(5500, employee.CalculateSalary()); // 5000 + 10%
            _mockEmployeeRepository.Verify(repo => repo.AddAsync(employee), Times.Once);
        }
    }
}
