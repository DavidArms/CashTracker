using CashTracker.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CashTracker.Database
{
    /// <summary>
    /// Generic base sqlite repository
    /// </summary>
    /// <typeparam name="T">Generic type parameter to be set by subclasses</typeparam>
    public abstract class BaseSQLiteRepository<T> : IAsyncRepository<T> where T : BaseEntity, new() // TODO: this new() makes everything work.... but why?
    {
        /// <summary>
        /// The connection to the database
        /// </summary>
        private SQLiteAsyncConnection Connection => DependencyService.Get<ISQLiteDb>().GetAsyncConnection();

        /// <summary>
        /// Constructor for the repository
        /// </summary>
        /// <remarks>Creates the table we need if it doesn't already exist in the DB</remarks>
        public BaseSQLiteRepository()
        {
            Connection.CreateTableAsync<T>();
        }

        /// <inheritdoc/>
        public async Task<T> GetByIdAsync(int id) => await Connection.Table<T>().FirstOrDefaultAsync(entity => entity.ID == id);

        /// <inheritdoc/>
        public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
            => Connection.Table<T>().FirstOrDefaultAsync(predicate);

        /// <inheritdoc/>
        public async Task AddAsync(T entity) => await Connection.InsertAsync(entity);

        /// <inheritdoc/>
        public async Task UpdateAsync(T entity) => await Connection.UpdateAsync(entity);

        /// <inheritdoc/>
        public async Task RemoveAsync(T entity) => await Connection.DeleteAsync(entity);

        /// <inheritdoc/>
        public async Task<IEnumerable<T>> GetAllAsync() => await Connection.Table<T>().ToListAsync();

        /// <inheritdoc/>
        public async Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate) =>
            await Connection.Table<T>().Where(predicate).ToListAsync();

        /// <inheritdoc/>
        public async Task<int> CountAll() => await Connection.Table<T>().CountAsync();

        /// <inheritdoc/>
        public Task<int> CountWhere(Expression<Func<T, bool>> predicate) => Connection.Table<T>().CountAsync(predicate);


    }
}
