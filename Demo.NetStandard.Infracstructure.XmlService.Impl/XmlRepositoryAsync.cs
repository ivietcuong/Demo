using Demo.NetStandard.Core.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Demo.NetStandard.Infracstructure.XmlService.Impl
{
	public class XmlRepositoryAsync : IRepositoryAsync
	{

		public IUnitOfWork UnitOfWork { get; }

		public XmlRepositoryAsync()
		{
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

		public Task<IEnumerable<T>> GetAsync<T>(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
		{
			throw new NotImplementedException();
		}
	}
}
