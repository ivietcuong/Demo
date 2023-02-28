using Demo.NetStandard.Core.Interfaces;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.NetStandard.Infrast.XmlService.Impl
{
    public class XmlContext : IUnitOfWork
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
