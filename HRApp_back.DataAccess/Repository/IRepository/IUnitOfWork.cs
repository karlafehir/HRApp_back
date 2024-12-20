using System;
using System.Threading.Tasks;
using HRApp_back.Models.Models;

namespace HRApp_back.DataAccess.Repository.IRepository;

public interface IUnitOfWork : IDisposable
{
    IJobRepository Jobs { get; }
    ICandidateRepository Candidates { get; }
    IEmployeeRepository Employees { get; }
    IDepartmentRepository Departments { get; }
    Task<int> SaveAsync();
}