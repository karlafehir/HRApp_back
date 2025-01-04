using System.Collections.Generic;
using HRApp_back.Models.Models;

namespace HRApp_back.DataAccess.Repository.IRepository;

public interface IEmployeeRepository : IRepository<Employee>
{
    public List<EmployeeWithRoleDto> GetEmployeesWithRoles(string roleName);
}