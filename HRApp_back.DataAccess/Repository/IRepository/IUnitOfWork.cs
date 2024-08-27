using System;
using System.Threading.Tasks;
using HRApp_back.Models.Models;

namespace HRApp_back.DataAccess.Repository.IRepository;

public interface IUnitOfWork : IDisposable
{
    IJobRepository Jobs { get; }
    Task<int> SaveAsync();
}