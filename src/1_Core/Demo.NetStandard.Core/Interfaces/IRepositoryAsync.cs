using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.NetStandard.Core.Interfaces
{
	public interface IRepositoryAsync
	{
		IUnitOfWork UnitOfWork { get; }
		Task<T> AddAsync<T>(T entity);
		Task UpdateAsync<T>(T entity);
		Task<T> GetByIdAsync<T>(int id);
		Task<int> DeleteAsync<T>(T entity);
		Task<IEnumerable<T>> GetAsync<T>(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = default);
	}
}
