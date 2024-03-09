using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces;
public interface IUnitOfWork : IAsyncDisposable
{
    IGenericRepository<T> Repository<T>() where T : class;
    void DatabaseRollback();
    void DatabaseCommit();
    Task<int> CompleteAsync();
    Task<int> ExecuteSqlRawAsync(string Query);
}