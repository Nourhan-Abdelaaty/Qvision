using MVC.Domain.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces;
public interface IGenericRepository<T> where T : class
{
    bool Add(T entity);
    bool AddRange(IEnumerable<T> entity);
    Task<ResponseResult> UpdateAsync(T entity);
    bool Remove(T entity);
    IQueryable<T> Where(Expression<Func<T, bool>> expression);
    List<T> ToList();
    T FindById(int id);
    T FirstOrDefault(Expression<Func<T, bool>> expression);
    T LastOrDefault(Expression<Func<T, bool>> expression);
    IQueryable<T> Include(params Expression<Func<T, object>>[] includes);
    IQueryable<TType> Select<TType>(Expression<Func<T, TType>> select);
    bool Any(Expression<Func<T, bool>> expression = null);
    Task<bool> AnyAsync(Expression<Func<T, bool>> expression = null);
    double Sum(Expression<Func<T, double>> expression);
    IQueryable<T> Take(int count);
    IQueryable<T> Skip(int count);
    int Count(Expression<Func<T, bool>> expression = null);
    int Max(Expression<Func<T, int>> expression);
    IQueryable<T> OrderBy(Expression<Func<T, int>> expression);
    Task<List<T>> ToListAsync();
    Task<T> FindByIdAsync(int id);
    Task<bool> AddAsync(T entity);
    Task<bool> AddRangeAsync(ICollection<T> entity);
    Task<ResponseResult> DeleteAsync(int id);
    bool DeleteRange(List<T> entity);
    IQueryable<T> OrderByDescending(Expression<Func<T, object>> expression);
    Task<int> MaxAsync(Expression<Func<T, int>> expression);
    Task<int> CountAsync();
    Task<ResponseResult> PagenationData(int page , int pageSize);
}