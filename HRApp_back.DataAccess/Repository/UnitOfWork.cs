using System.Threading.Tasks;
using HRApp_back.DataAccess.Data;
using HRApp_back.DataAccess.Repository.IRepository;

namespace HRApp_back.DataAccess.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public IJobRepository Jobs { get; }
    
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Jobs = new JobRepository(_context);
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
