using HRApp_back.DataAccess.Repository.IRepository;
using HRApp_back.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRApp_back.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public EmployeeController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet("GetAllEmployees")]
    public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
    {
        var employees = await _unitOfWork.Employees.GetAllAsync(includeProperties: "Job");
        
        return Ok(employees);
    }
    
    [HttpGet("GetEmployeeById/{id}")]
    public async Task<ActionResult<Employee>> GetEmployeeById(int id)
    {
        var employee = await _unitOfWork.Employees.GetByIdAsync(id, includeProperties: "Job");

        if (employee == null)
        {
            return NotFound();
        }

        return Ok(employee);
    }

    [HttpPost("AddEmployee")]
    public async Task<ActionResult<Employee>> AddEmployee(Employee employee)
    {
        await _unitOfWork.Employees.AddAsync(employee);
        await _unitOfWork.SaveAsync();

        return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
    }

    [HttpPut("UpdateEmployee/{id}")] 
    public async Task<IActionResult> UpdateEmployee(int id, Employee employee)
    {
        if (id != employee.Id)
        {
            return BadRequest();
        }

        _unitOfWork.Employees.Update(employee);

        try
        {
            await _unitOfWork.SaveAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!(await EmployeeExists(id)))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("DeleteEmployee/{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        var employee = await _unitOfWork.Employees.GetByIdAsync(id);
        if (employee == null)
        {
            return NotFound();
        }

        _unitOfWork.Employees.Delete(employee);
        await _unitOfWork.SaveAsync();

        return Ok();
    }

    private async Task<bool> EmployeeExists(int id)
    {
        var employee = await _unitOfWork.Employees.GetByIdAsync(id);
        return employee != null;
    }
}
