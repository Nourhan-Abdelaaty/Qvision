using Application.Services.Interfaces;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MVC.DataAccess.Context;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.UnitOfWork;
public class UnitOfWork : IUnitOfWork
{
    private Hashtable _repositories;
    private readonly DataContext _context;
    public UnitOfWork(DataContext context)
    {
        _context = context;
    }
    public void DatabaseRollback()
    {
        _context.Database.BeginTransaction().Rollback();
    }
    public void DatabaseCommit()
    {
        _context.Database.BeginTransaction().Commit();
    }
    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }
    public async Task<int> ExecuteSqlRawAsync(string Query)
    {
        return await _context.Database.ExecuteSqlRawAsync(Query);
    }
    public IGenericRepository<T> Repository<T>() where T : class
    {
        if (_repositories == null) _repositories = new Hashtable();

        var type = typeof(T).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(GenericRepository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);
            _repositories.Add(type, repositoryInstance);
        }
        return (IGenericRepository<T>)_repositories[type];
    }
    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        await _context.DisposeAsync();
        await ValueTask.CompletedTask;
    }
}

