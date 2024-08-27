using HRApp_back.DataAccess.Data;
using HRApp_back.Models.Models;

namespace HRApp_back.DataAccess.Repository.IRepository;

public class CandidateRepository : Repository<Candidate>, ICandidateRepository
{
    private readonly ApplicationDbContext _context;
    
    public CandidateRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}