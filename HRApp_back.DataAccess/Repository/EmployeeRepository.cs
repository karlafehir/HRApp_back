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
}