using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace Demo.NetStandard.Core.Interfaces
{
	public interface IUnitOfWork
	{
		int SaveChanges(bool acceptAllChangesOnSuccess);
		Task<IEnumerable<T>> SetAsync<T>() where T : class;
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}