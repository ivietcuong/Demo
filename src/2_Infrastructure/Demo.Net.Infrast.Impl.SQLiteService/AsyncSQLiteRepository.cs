using Demo.NetStandard.Core.Interfaces;

using System.Linq.Expressions;

namespace Demo.Net.Infrast.Impl.SQLiteService
{
	public class AsyncSQLiteRepository : IAsyncRepository
	{
		private readonly IUnitOfWork _context;

		public IUnitOfWork UnitOfWork 
		{
			get => _context; 
		}

		public AsyncSQLiteRepository(IUnitOfWork unitOfWork)
		{
			_context = unitOfWork;
		}

		public Task<T> AddAsync<T>(T entity)
		{
			throw new NotImplementedException();
		}

		public Task UpdateAsync<T>(T entity)
		{
			throw new NotImplementedException();
		}

		public Task<T> GetByIdAsync<T>(int id)
		{
			throw new NotImplementedException();
		}

		public Task<int> DeleteAsync<T>(T entity)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<T>> GetAsync<T>(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null) where T : class
		{
			IEnumerable<T> set = await _context.SetAsync<T>();

			if (set == null)
				return Enumerable.Empty<T>();

			if (predicate != null)
				set = set.AsQueryable().Where(predicate);

			if (orderBy != null)
				return orderBy(set.AsQueryable()).ToList();

			return set.ToList();
		}
	}
}
