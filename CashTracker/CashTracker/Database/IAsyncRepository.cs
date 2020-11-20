using CashTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CashTracker.Database
{
    /// <summary>
    /// Interface defining an asynchronous database repository
    /// </summary>
    /// <typeparam name="T">A generic type param which must be of type <see cref="BaseEntity"/></typeparam>
    public interface IAsyncRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Get a <typeparamref name="T"/> record which has the provided id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Returns the first or default result matching the provided predicate of type <typeparamref name="T"/>
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Adds the passed in entity of type <typeparamref name="T"/> to the database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task AddAsync(T entity);

        /// <summary>
        /// Updates the provided entity in the database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Removes the passed in object from the database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task RemoveAsync(T entity);

        /// <summary>
        /// Gets all records in the <typeparamref name="T"/> table
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Gets all records in the <typeparamref name="T"/> table which match the passed in predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Gets the count of all records of type <see cref="T"/>
        /// </summary>
        /// <returns></returns>
        Task<int> CountAll();

        /// <summary>
        /// Gets the count of all <typeparamref name="T"/> which are returned by the <paramref name="predicate"/>
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<int> CountWhere(Expression<Func<T, bool>> predicate);
    }
}
