using System.Threading.Tasks;
using HRApp_back.DataAccess.Data;
using HRApp_back.DataAccess.Repository.IRepository;
using HRApp_back.Models.Models;

namespace HRApp_back.DataAccess.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public IJobRepository Jobs { get; }
    public ICandidateRepository Candidates { get; }
    public IEmployeeRepository Employees { get; }
    public IDepartmentRepository Departments { get; }
    public IEmployeeLeaveRecordRepository EmployeeLeaveRecords { get; }
    public IProjectRepository Projects { get; }
    
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Jobs = new JobRepository(_context);
        Candidates = new CandidateRepository(_context);
        Employees = new EmployeeRepository(_context);
        Departments = new DepartmentRepository(_context);
        EmployeeLeaveRecords = new EmployeeLeaveRecordRepository(_context);
        Projects = new ProjectRepository(_context);
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
