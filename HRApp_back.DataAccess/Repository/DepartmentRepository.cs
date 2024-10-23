using HRApp_back.DataAccess.Data;
using HRApp_back.DataAccess.Repository.IRepository;
using HRApp_back.Models.Models;

namespace HRApp_back.DataAccess.Repository;

public class DepartmentRepository : Repository<Department>, IDepartmentRepository
{
    private readonly ApplicationDbContext _context;
    
    public DepartmentRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}