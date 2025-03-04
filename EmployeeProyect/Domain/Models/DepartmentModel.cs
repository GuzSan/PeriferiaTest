namespace EmployeeProyect.Domain.Models
{
    public class DepartmentModel
    {

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<EmployeeModel> Employees { get; set; } = new();
    }
}
