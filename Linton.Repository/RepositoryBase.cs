
using Linton.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Linton.Repository { 

    public class RepositoryBase<T> : IRepository<T> where T : class, new()
    {

        protected LintonDbContext _context;
        internal DbSet<T> dbSet;
        public RepositoryBase(LintonDbContext context)
        {
            this._context = context;
            this.dbSet = context.Set<T>();
        }

        public virtual IEnumerable<T> All()
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> Delete(int ID)
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

        public virtual  IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return (IQueryable<T>)dbSet.Where(predicate).ToListAsync();
        }

        public virtual Task<bool> Save(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
