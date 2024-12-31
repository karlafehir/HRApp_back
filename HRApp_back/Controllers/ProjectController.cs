using HRApp_back.DataAccess.Data;
using HRApp_back.DataAccess.Repository.IRepository;
using HRApp_back.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRApp_back.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public ProjectsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // GET: api/Projects/GetAllProjects
    [HttpGet("GetAllProjects")]
    public async Task<ActionResult<IEnumerable<Project>>> GetAllProjects()
    {
        var projects = await _unitOfWork.Projects.GetAllAsync();
        return Ok(projects);
    }

    // POST: api/Projects/AddProject
    [HttpPost("AddProject")]
    public async Task<ActionResult<Project>> CreateProject(Project project)
    {
        if (project == null) return BadRequest("Project cannot be null");

        project.StartDate = project.StartDate == DateTime.MinValue ? DateTime.UtcNow : project.StartDate;
        
        await _unitOfWork.Projects.AddAsync(project);
        await _unitOfWork.SaveAsync();

        return CreatedAtAction(nameof(GetProjectById), new { id = project.Id }, project);
    }

    // GET: api/Projects/GetProjectById/{id}
    [HttpGet("GetProjectById/{id}")]
    public async Task<ActionResult<Project>> GetProjectById(int id)
    {
        var project = await _unitOfWork.Projects.GetByIdAsync(id);
        if (project == null) return NotFound();

        return Ok(project);
    }
    
    [HttpGet("{projectId}/employees")]
    public async Task<IActionResult> GetEmployeesByProjectId(int projectId)
    {
        var employees = await _unitOfWork.Projects.GetEmployeesByProjectId(projectId);
        if (employees == null || !employees.Any())
        {
            return NotFound($"No employees found for project with ID {projectId}.");
        }

        return Ok(employees);
    }


    // // GET: api/Projects/GetProjectByIdWithEmployees/{id}
    // [HttpGet("GetProjectByIdWithEmployees/{id}")]
    // public async Task<ActionResult<Project>> GetProjectByIdWithEmployees(int id)
    // {
    //     var project = await _unitOfWork.Projects.GetProjectWithEmployeesAsync(id);
    //     if (project == null) return NotFound();
    //
    //     return Ok(project);
    // }

    // PUT: api/Projects/UpdateProject/{id}
    [HttpPut("UpdateProject/{id}")]
    public async Task<IActionResult> UpdateProject(int id, Project project)
    {
        if (id != project.Id)
        {
            return BadRequest("Project ID mismatch.");
        }

        _unitOfWork.Projects.Update(project);

        try
        {
            await _unitOfWork.SaveAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!(await ProjectExists(id)))
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

    // DELETE: api/Projects/DeleteProject/{id}
    [HttpDelete("DeleteProject/{id}")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        var project = await _unitOfWork.Projects.GetByIdAsync(id);
        if (project == null)
        {
            return NotFound();
        }

        _unitOfWork.Projects.Delete(project);
        await _unitOfWork.SaveAsync();

        return NoContent();
    }

    // Helper method to check if a project exists
    private async Task<bool> ProjectExists(int id)
    {
        var project = await _unitOfWork.Projects.GetByIdAsync(id);
        return project != null;
    }
}
