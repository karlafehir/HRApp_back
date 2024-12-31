using System.Collections.Generic;
using System.Threading.Tasks;
using HRApp_back.Models.Models;

namespace HRApp_back.DataAccess.Repository.IRepository;

public interface IProjectRepository : IRepository<Project>
{
    Task<IEnumerable<Employee>> GetEmployeesByProjectId(int projectId); 
}