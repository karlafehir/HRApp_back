using System.Collections.Generic;
using System.Threading.Tasks;
using HRApp_back.Models.Models;

namespace HRApp_back.DataAccess.Repository.IRepository;

public interface IJobRepository : IRepository<Job>
{
    Task<IEnumerable<Job>> GetActiveJobsAsync();
}