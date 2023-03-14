using Demo.Net.Core.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Demo.Net.Infrast.Impl.XmlService
{
	public class AsyncXmlRepository : IAsyncRepository
	{
		private readonly IUnitOfWork _context;
		public IUnitOfWork UnitOfWork 
		{
			get => _context;
		}

		public AsyncXmlRepository(IUnitOfWork unitOfWork)
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

		public async Task<IEnumerable<T>> GetAsync<T>(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
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
