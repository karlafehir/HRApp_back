using HRApp_back.DataAccess.Data;
using HRApp_back.Models.Models;

namespace HRApp_back.DataAccess.Repository.IRepository;

public class PayrollRepository : Repository<Payroll>, IPayrollRepository
{
    private readonly ApplicationDbContext _context;
    
    public PayrollRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}