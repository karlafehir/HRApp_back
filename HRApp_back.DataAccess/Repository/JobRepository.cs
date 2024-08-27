using System.Collections.Generic;
using System.Threading.Tasks;
using HRApp_back.DataAccess.Data;
using HRApp_back.DataAccess.Repository.IRepository;
using HRApp_back.Models.Models;

namespace HRApp_back.DataAccess.Repository;

public class JobRepository : Repository<Job>, IJobRepository
{
    private readonly ApplicationDbContext _context;
    
    public JobRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public Task<IEnumerable<Job>> GetActiveJobsAsync()
    {
        throw new System.NotImplementedException();
    }
}
