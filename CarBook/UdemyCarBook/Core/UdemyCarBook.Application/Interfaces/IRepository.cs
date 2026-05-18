using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace UdemyCarBook.Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
            Task<List<T>> GetAllAsync();
            Task<T> GetByIdAsync(int id);
            Task CreateAsync(T entity);
            Task UpdateAsync(T entity);
            Task RemoveAsync(T entity);
            Task<T?> GetByFilterAsync(Expression<Func<T, bool>> filter);
        Task<List<T>> GetListByFilterAsync(Expression<Func<T, bool>> filter);
        IQueryable<T> GetQueryable(); // Include yapabilmek için bu çok önemli!
    }


}
