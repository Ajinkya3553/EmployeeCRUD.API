using EmployeeCRUD.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCRUD.API.Data
{
    public class EmployeeCRUDDbContext : DbContext
    {
        public EmployeeCRUDDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
