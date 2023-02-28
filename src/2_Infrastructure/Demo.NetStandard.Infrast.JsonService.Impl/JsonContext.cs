using Demo.NetStandard.Core.Interfaces;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.NetStandard.Infrast.JsonService.Impl
{
    public class JsonContext : IUnitOfWork
    {
        public Task<IEnumerable<T>> SetAsync<T>()
        {
            throw new NotImplementedException();
        }

        public int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
