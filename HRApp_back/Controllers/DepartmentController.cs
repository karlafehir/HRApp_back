using HRApp_back.DataAccess.Repository.IRepository;
using HRApp_back.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRApp_back.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepartmentController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public DepartmentController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet("GetDepartments")]
    public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
    {
        var departments = await _unitOfWork.Departments.GetAllAsync(includeProperties: "Manager,Employees");
        return Ok(departments);
    }

    [HttpGet("GetDepartmentById/{id}")]
    public async Task<ActionResult<Department>> GetDepartmentById(int id)
    {
        var department = await _unitOfWork.Departments.GetByIdAsync(id, includeProperties: "Manager,Employees");

        if (department == null)
        {
            return NotFound();
        }

        return Ok(department);
    }

    [HttpPost("AddDepartment")]
    public async Task<ActionResult<Department>> AddDepartment(Department department)
    {
        await _unitOfWork.Departments.AddAsync(department);
        await _unitOfWork.SaveAsync();

        return CreatedAtAction(nameof(GetDepartmentById), new { id = department.Id }, department);
    }

    [HttpPut("UpdateDepartment/{id}")]
    public async Task<IActionResult> UpdateDepartment(int id, Department department)
    {
        if (id != department.Id)
        {
            return BadRequest();
        }

        _unitOfWork.Departments.Update(department);

        try
        {
            await _unitOfWork.SaveAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!(await DepartmentExists(id)))
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

    [HttpDelete("DeleteDepartment/{id}")]
    public async Task<IActionResult> DeleteDepartment(int id)
    {
        var department = await _unitOfWork.Departments.GetByIdAsync(id);
        if (department == null)
        {
            return NotFound();
        }

        _unitOfWork.Departments.Delete(department);
        await _unitOfWork.SaveAsync();

        return Ok();
    }

    private async Task<bool> DepartmentExists(int id)
    {
        var department = await _unitOfWork.Departments.GetByIdAsync(id);
        return department != null;
    }
}
