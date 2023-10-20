using EmployeeCRUD.API.Data;
using EmployeeCRUD.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EmployeeCRUD.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly EmployeeCRUDDbContext _employeeCRUDDbContext;
        public EmployeesController(EmployeeCRUDDbContext employeeCRUDDbContext)
        {
            _employeeCRUDDbContext = employeeCRUDDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeCRUDDbContext.Employees.ToListAsync();

            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employeeRequest)
        {
            employeeRequest.Id = Guid.NewGuid();

            await _employeeCRUDDbContext.Employees.AddAsync(employeeRequest);
            await _employeeCRUDDbContext.SaveChangesAsync();

            return Ok(employeeRequest);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var employee = await _employeeCRUDDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, Employee updateEmployeeRequest)
        {
            var employee = await _employeeCRUDDbContext.Employees.FindAsync(id);

            if(employee == null)
            {
                return NotFound();
            }

            employee.Name = updateEmployeeRequest.Name;
            employee.Email = updateEmployeeRequest.Email;
            employee.Phone = updateEmployeeRequest.Phone;
            employee.Salary = updateEmployeeRequest.Salary;
            employee.Department = updateEmployeeRequest.Department;

            await _employeeCRUDDbContext.SaveChangesAsync();

            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var employee = await _employeeCRUDDbContext.Employees.FindAsync(id);

            if(employee == null)
            {
                return NotFound();
            }

            _employeeCRUDDbContext.Employees.Remove(employee);
            await _employeeCRUDDbContext.SaveChangesAsync();
            return Ok(employee);
        }
    }
}











