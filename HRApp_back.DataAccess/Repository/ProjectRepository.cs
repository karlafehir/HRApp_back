using System.Collections.Generic;
using System.Threading.Tasks;
using HRApp_back.DataAccess.Data;
using HRApp_back.DataAccess.Repository.IRepository;
using HRApp_back.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace HRApp_back.DataAccess.Repository;

public class ProjectRepository : Repository<Project>, IProjectRepository
{
    private readonly ApplicationDbContext _context;
    
    public ProjectRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    
    // [HttpGet("{projectId}/employees")]
    // public async Task<IActionResult> GetEmployeesByProjectId(int projectId)
    // {
    //     var employees = await _context.Employees
    //         .Where(e => e.ProjectId == projectId)
    //         .ToListAsync();
    //
    //     return Ok(employees);
    // }

}
