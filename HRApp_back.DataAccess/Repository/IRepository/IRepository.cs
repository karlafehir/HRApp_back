using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRApp_back.DataAccess.Repository.IRepository;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync(string? includeProperties = null);
    Task<T> GetByIdAsync(int id, string? includeProperties = null);
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}