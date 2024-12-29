using HRApp_back.DataAccess.Data;
using HRApp_back.Models.Models;

namespace HRApp_back.DataAccess.Repository.IRepository;

public class EmployeeLeaveRecordRepository : Repository<EmployeeLeaveRecord>, IEmployeeLeaveRecordRepository
{
    private readonly ApplicationDbContext _context;
    
    public EmployeeLeaveRecordRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}