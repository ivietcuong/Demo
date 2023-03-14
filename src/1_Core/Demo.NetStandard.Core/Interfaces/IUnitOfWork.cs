using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace Demo.Net.Core.Interfaces
{
	public interface IUnitOfWork
	{
		Task<IEnumerable<T>> SetAsync<T>();
		int SaveChanges(bool acceptAllChangesOnSuccess);
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}