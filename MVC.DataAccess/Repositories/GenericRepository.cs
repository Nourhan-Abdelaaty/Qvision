using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Application.Services.Interfaces;
using MVC.DataAccess.Context;
using MVC.Domain.Helper;
using MVC.Domain.Models;

namespace DataAccess.Repositories;
public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
{
    protected DataContext _context;
    private readonly DbSet<T> Table;

    public GenericRepository(DataContext context)
    {
        _context = context;
        Table = _context.Set<T>();
    }
    public T FindById(int id)
    {
        return _context.Set<T>().Find(id);
    }
    public bool Add(T entity)
    {
        if (entity == null)
        {
            throw new NullReferenceException();
        }
        Table.Add(entity);
        return true;
    }
    public bool AddRange(IEnumerable<T> entity)
    {
        if (entity.Count() == 0)
        {
            throw new NullReferenceException();
        }
        Table.AddRange(entity);
        return true;
    }
    public async Task<ResponseResult> UpdateAsync(T entity)
    {
        if (entity == null)
        {
            throw new NullReferenceException();
        }
        Table.Update(entity);
        return new ResponseResult { IsSuccess = true, Message = "Update Success" };
     }
    public bool Remove(T entity)
    {
        if (entity == null)
        {
            throw new NullReferenceException();
        }
        Table.Remove(entity);
        return true;
    }
    public IQueryable<T> Where(Expression<Func<T, bool>> expression)
    {
        return Table.Where(expression);
    }
    public List<T> ToList()
    {
        return Table.ToList();
    }
    public T FirstOrDefault(Expression<Func<T, bool>> expression)
    {
        if (expression != null)
            return Table.FirstOrDefault(expression);
        else
            return Table.FirstOrDefault();
    }
    public T LastOrDefault(Expression<Func<T, bool>> expression)
    {
        if (expression != null)
            return Table.LastOrDefault(expression);
        else
            return Table.LastOrDefault();
    }
    public IQueryable<T> Include(params Expression<Func<T, object>>[] includes)
    {
        IIncludableQueryable<T, object> query = null;
        foreach (var include in includes)
            query = Table.Include(include);
        return query;
    }
    public IQueryable<TType> Select<TType>(Expression<Func<T, TType>> select)
    {
        return Table.Select(select);
    }
    public bool Any(Expression<Func<T, bool>> expression = null)
    {
        if (expression != null)
            return Table.Any(expression);
        else
            return Table.Any();
    }
    public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression = null)
    {
        if (expression != null)
            return await Table.AnyAsync(expression);
        else
            return await Table.AnyAsync();
    }
    public double Sum(Expression<Func<T, double>> expression)
    {
        return Table.Sum(expression);
    }
    public IQueryable<T> Take(int count)
    {
        return Table.Take(count);
    }
    public IQueryable<T> Skip(int count)
    {
        return Table.Skip(count);

    }
    public int Count(Expression<Func<T, bool>> expression = null)
    {
        return Table.Count(expression);
    }
    public int Max(Expression<Func<T, int>> expression)
    {
        try
        {
            return Table.Max(expression);
        }
        catch (Exception)
        {
            return 0;
        }
    }
    public IQueryable<T> OrderBy(Expression<Func<T, int>> expression)
    {
        return Table.OrderBy(expression);
    }
    public async Task<List<T>> ToListAsync()
    {
        var list = await Table.ToListAsync();
        return list;
    }
    public async Task<T> FindByIdAsync(int id)
    {
        var model = await Table.FindAsync(id);
        return model;
    }
    public async Task<bool> AddAsync(T entity)
    {
        if (entity == null)
        {
            throw new NullReferenceException();
        }
        await Table.AddAsync(entity);
        return true;
    }
    public async Task<bool> AddRangeAsync(ICollection<T> entity)
    {
        if (entity.Count() == 0)
        {
            throw new NullReferenceException();
        }
        await Table.AddRangeAsync(entity);
        return true;
    }
    public async Task<ResponseResult> DeleteAsync(int id)
    {
        var model = await FindByIdAsync(id);
        if (model == null)
        {
            return new ResponseResult { IsSuccess = false, Message = "notfound" };
        }
        Remove(model);
        return new ResponseResult { IsSuccess = true, Message = "delete success" };
    }
    public bool DeleteRange(List<T> entity)
    {
        try
        {
            Table.RemoveRange(entity);
            return true;
        }
        catch (Exception)
        {
            return false;
            throw;
        }
    }
    public IQueryable<T> OrderByDescending(Expression<Func<T, object>> expression)
    {
        return Table.OrderByDescending(expression);
    }
    public async Task<int> MaxAsync(Expression<Func<T, int>> expression)
    {
        var result = 0;
        try
        {
            result = await Table.MaxAsync(expression);
        }
        catch (Exception)
        {
        }
        return result;
    }
    public async Task<int> CountAsync()
    {
        return await Table.CountAsync();
    }
    public async Task<ResponseResult> PagenationData(int page, int pageSize )
    {
        var data = Table.OrderBy(e => e.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        return new ResponseResult { IsSuccess = true ,Obj =data};
    }
}

