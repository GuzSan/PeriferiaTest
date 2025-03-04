using EmployeeProyect.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeProyect.Infrastucture.Persistence
{
    public class EmployeeDBContext : DbContext
    {
        public EmployeeDBContext(DbContextOptions<EmployeeDBContext> options) : base(options) { }

        public DbSet<EmployeeModel> Employees { get; set; }
        public DbSet<DepartmentModel> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DepartmentModel>()
                .HasMany(d => d.Employees)
                .WithOne(e => e.Department)
                .HasForeignKey(e => e.DepartmentId);

            modelBuilder.Entity<EmployeeModel>()
                .Property(e => e.Position)
                .HasConversion<string>();
        }
    }
}
