using System.Collections.Generic;
using System.Threading.Tasks;
using HRApp_back.DataAccess.Repository.IRepository;
using HRApp_back.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRApp_back.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<IdentityUser> _userManager;

    public EmployeeController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }

    // Get all employees
    [HttpGet("GetAllEmployees")]
    public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
    {
        var employees = await _unitOfWork.Employees.GetAllAsync(includeProperties: "Job,EmployeeLeaveRecord,Project");
        return Ok(employees);
    }

    // Get employee by ID
    [HttpGet("GetEmployeeById/{id}")]
    public async Task<ActionResult<Employee>> GetEmployeeById(int id)
    {
        var employee = await _unitOfWork.Employees.GetByIdAsync(id, includeProperties: "Job,EmployeeLeaveRecord,Project");

        if (employee == null)
        {
            return NotFound("Employee not found.");
        }

        return Ok(employee);
    }

    // Add a new employee
    [HttpPost("AddEmployee")]
    public async Task<ActionResult<Employee>> AddEmployee(Employee employee)
    {
        if (employee.EmployeeLeaveRecord == null)
        {
            employee.EmployeeLeaveRecord = new EmployeeLeaveRecord
            {
                AnnualLeaveDays = 20,
                SickLeaveDays = 10,
                RemainingAnnualLeave = 20,
                RemainingSickLeave = 10
            };
        }

        // Add employee to the database
        await _unitOfWork.Employees.AddAsync(employee);
        await _unitOfWork.SaveAsync();

        // Create a corresponding IdentityUser
        var identityUser = new IdentityUser
        {
            UserName = employee.Email,
            Email = employee.Email,
            EmailConfirmed = true
        };

        // Use a single, predefined password for all employees
        var predefinedPassword = "Test123!";
        var result = await _userManager.CreateAsync(identityUser, predefinedPassword);

        if (!result.Succeeded)
        {
            // Rollback: Remove the employee if IdentityUser creation fails
            _unitOfWork.Employees.Delete(employee);
            await _unitOfWork.SaveAsync();
            return BadRequest(result.Errors);
        }

        // Assign the "Employee" role to the user
        await _userManager.AddToRoleAsync(identityUser, "Employee");

        return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
    }

    // Get employees with roles
    [HttpGet("GetEmployeesWithRoles")]
    public IActionResult GetEmployeesWithRoles([FromQuery] string roleName = null)
    {
        try
        {
            var employees = _unitOfWork.Employees.GetEmployeesWithRoles(roleName);
            return Ok(employees);
        }
        catch (Exception ex)
        {
            // Log the exception as needed
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

    // Update an employee
    [HttpPut("UpdateEmployee/{id}")]
    public async Task<IActionResult> UpdateEmployee(int id, Employee employee)
    {
        if (id != employee.Id)
        {
            return BadRequest("Employee ID mismatch.");
        }

        // Retrieve the existing employee with their leave record
        var existingEmployee = await _unitOfWork.Employees.GetByIdAsync(id, includeProperties: "EmployeeLeaveRecord");

        if (existingEmployee == null)
        {
            return NotFound("Employee not found.");
        }

        // Update employee details
        existingEmployee.FirstName = employee.FirstName;
        existingEmployee.LastName = employee.LastName;
        existingEmployee.Email = employee.Email;
        existingEmployee.Address = employee.Address;
        existingEmployee.PhoneNumber = employee.PhoneNumber;
        existingEmployee.DateOfHire = employee.DateOfHire;
        existingEmployee.JobId = employee.JobId;
        existingEmployee.DepartmentId = employee.DepartmentId;
        existingEmployee.ProjectId = employee.ProjectId;
        existingEmployee.Salary = employee.Salary;
        existingEmployee.EmploymentStatus = employee.EmploymentStatus;

        // Update or initialize the EmployeeLeaveRecord
        if (employee.EmployeeLeaveRecord != null)
        {
            if (existingEmployee.EmployeeLeaveRecord != null)
            {
                // Update the existing leave record
                existingEmployee.EmployeeLeaveRecord.AnnualLeaveDays = employee.EmployeeLeaveRecord.AnnualLeaveDays;
                existingEmployee.EmployeeLeaveRecord.SickLeaveDays = employee.EmployeeLeaveRecord.SickLeaveDays;
                existingEmployee.EmployeeLeaveRecord.RemainingAnnualLeave = employee.EmployeeLeaveRecord.RemainingAnnualLeave;
                existingEmployee.EmployeeLeaveRecord.RemainingSickLeave = employee.EmployeeLeaveRecord.RemainingSickLeave;
            }
            else
            {
                // Add a new leave record
                existingEmployee.EmployeeLeaveRecord = new EmployeeLeaveRecord
                {
                    AnnualLeaveDays = employee.EmployeeLeaveRecord.AnnualLeaveDays,
                    SickLeaveDays = employee.EmployeeLeaveRecord.SickLeaveDays,
                    RemainingAnnualLeave = employee.EmployeeLeaveRecord.RemainingAnnualLeave,
                    RemainingSickLeave = employee.EmployeeLeaveRecord.RemainingSickLeave,
                    EmployeeId = existingEmployee.Id
                };
            }
        }

        try
        {
            await _unitOfWork.SaveAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!(await EmployeeExists(id)))
            {
                return NotFound("Employee not found.");
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // Delete an employee
    [HttpDelete("DeleteEmployee/{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        var employee = await _unitOfWork.Employees.GetByIdAsync(id);
        if (employee == null)
        {
            return NotFound("Employee not found.");
        }

        // Remove the corresponding IdentityUser
        var identityUser = await _userManager.FindByEmailAsync(employee.Email);
        if (identityUser != null)
        {
            await _userManager.DeleteAsync(identityUser);
        }

        _unitOfWork.Employees.Delete(employee);
        await _unitOfWork.SaveAsync();

        return Ok("Employee deleted.");
    }

    // Helper method: Check if an employee exists
    private async Task<bool> EmployeeExists(int id)
    {
        var employee = await _unitOfWork.Employees.GetByIdAsync(id);
        return employee != null;
    }
}
