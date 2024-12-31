using System.Collections.Generic;
using System.Linq;
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
    
    public async Task<IEnumerable<Employee>> GetEmployeesByProjectId(int projectId)
    {
        return await _context.Employees
            .Where(e => e.ProjectId == projectId)
            .ToListAsync();
    }

}
