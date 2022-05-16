using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Linton.Domain.Interfaces.Repositories
{
	public interface IRepository<T> where T : class, new()
	{
		Task<T> Get(int id); 
		Task<IEnumerable<T>> All(); 
		IQueryable<T> Where(Expression<Func<T, bool>> predicate); 
		Task<bool> Add(T entity);
		Task<bool> Save(T entity); 
		Task<bool> Update(T entity);
		Task<bool> Delete(int ID); 

	}
}
