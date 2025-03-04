using EmployeeProyect.Domain.Enums;

namespace EmployeeProyect.Domain.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public decimal BaseSalary { get; set; }
        public JobPosition Position { get; set; }
        public int DepartmentId { get; set; }
        public DepartmentModel? Department { get; set; }

        public decimal CalculateSalary()
        {
            return Position switch
            {
                JobPosition.Developer => BaseSalary * 1.1m,
                JobPosition.Manager => BaseSalary * 1.2m,
                JobPosition.HR or JobPosition.Sales => BaseSalary,
                _ => BaseSalary
            };
        }
    }
}
