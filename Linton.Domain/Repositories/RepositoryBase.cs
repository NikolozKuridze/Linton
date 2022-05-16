using Linton.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Linton.Domain.Repositories
{

    public class RepositoryBase<T> : IRepository<T> where T : class, new()
    {

        protected LintonDbContext _context;
        internal DbSet<T> dbSet;
        public RepositoryBase(LintonDbContext context)
        {
            this._context = context;
            this.dbSet = context.Set<T>();
        }

        public virtual Task<IEnumerable<T>> All()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int ID)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<T> Get(int id)
        {

            return await dbSet.FindAsync(id);
        }

        public virtual async Task<bool> Add(T entity)
        {
            await dbSet.AddAsync(entity);
            return true;
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return (IQueryable<T>)dbSet.Where(predicate).ToListAsync();
        }

        public virtual Task<bool> Save(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
