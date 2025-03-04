
using Moq;
using EmployeeProyect.Application.Interfaces;
using EmployeeProyect.Application.Services;
using EmployeeProyect.Domain.Models;

namespace EmployeeProyect.TestsAPI
{
    public class DepartmentServiceTests
    {
        private readonly Mock<IDepartmentRepository> _mockDepartmentRepository;
        private readonly DepartmentService _departmentService;

        public DepartmentServiceTests()
        {
            _mockDepartmentRepository = new Mock<IDepartmentRepository>();
            _departmentService = new DepartmentService(_mockDepartmentRepository.Object);
        }

        [Fact]
        public async Task GetAllDepartmentsAsync_ShouldReturnAllDepartments()
        {
            // Arrange
            var departments = new List<DepartmentModel>
            {
                new DepartmentModel { Id = 1, Name = "IT" },
                new DepartmentModel { Id = 2, Name = "HR" }
            };

            _mockDepartmentRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(departments);

            // Act
            var result = await _departmentService.GetAllDepartmentsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetDepartmentByIdAsync_ShouldReturnDepartment_WhenExists()
        {
            // Arrange
            var department = new DepartmentModel { Id = 1, Name = "IT" };

            _mockDepartmentRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(department);

            // Act
            var result = await _departmentService.GetDepartmentByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("IT", result.Name);
        }

        [Fact]
        public async Task GetDepartmentByIdAsync_ShouldReturnNull_WhenNotExists()
        {
            DepartmentModel dep = null;
            // Arrange
            _mockDepartmentRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(dep);

            // Act
            var result = await _departmentService.GetDepartmentByIdAsync(1);

            // Assert
            Assert.Null(result);
        }

       

        [Fact]
        public async Task AddDepartmentAsync_ShouldCallRepository()
        {
            // Arrange
            var department = new DepartmentModel { Id = 1, Name = "Finance" };

            // Act
            await _departmentService.AddDepartmentAsync(department);

            // Assert
            _mockDepartmentRepository.Verify(repo => repo.AddAsync(department), Times.Once);
        }
    }
}
