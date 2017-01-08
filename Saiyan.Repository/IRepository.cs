using Saiyan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Saiyan.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        bool Any(Expression<Func<T, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        int Count(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
        T First(Expression<Func<T, bool>> predicate);
        Task<T> FirstAsync(Expression<Func<T, bool>> predicate);
        T FirstOrDefault(Expression<Func<T, bool>> predicate, IFilter options);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, IFilter options);
        IEnumerable<T> Search(Expression<Func<T, bool>> predicate);
        IList<T> ToList(Expression<Func<T, bool>> predicate);
        IList<T> ToList(Expression<Func<T, bool>> predicate, IFilter filter);
        Task<IList<T>> ToListAsync(Expression<Func<T, bool>> predicate);
        Task<IList<T>> ToListAsync(Expression<Func<T, bool>> predicate, IFilter filter);
        void Insert(T item);
        Task InsertAsync(T item);
        bool Update(T item);
        Task<bool> UpdateAsync(T item);
        bool Delete(T item);
        Task<bool> DeleteAsync(T item);
    }
}
