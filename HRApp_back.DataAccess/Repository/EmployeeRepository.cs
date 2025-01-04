using System.Collections.Generic;
using System.Linq;
using HRApp_back.DataAccess.Data;
using HRApp_back.Models.Models;

namespace HRApp_back.DataAccess.Repository.IRepository;

public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
{
    private readonly ApplicationDbContext _context;
    
    public EmployeeRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    
    public List<EmployeeWithRoleDto> GetEmployeesWithRoles(string roleName = null)
    {
        var query = from e in _context.Employees
            join u in _context.Users on e.Email equals u.Email
            join ur in _context.UserRoles on u.Id equals ur.UserId
            join r in _context.Roles on ur.RoleId equals r.Id
            where roleName == null || r.Name == roleName
            select new EmployeeWithRoleDto
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                Role = r.Name 
            };

        return query.ToList();
    }

}

public class EmployeeWithRoleDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; } 
}

