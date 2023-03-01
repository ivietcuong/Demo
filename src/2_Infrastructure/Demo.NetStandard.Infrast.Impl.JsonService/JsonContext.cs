using Demo.NetStandard.Core.Entities;
using Demo.NetStandard.Core.Interfaces;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.NetStandard.Infrast.JsonService.Impl
{
    public class JsonContext : IUnitOfWork
    {
		public IEnumerable<Point> Points { get; set; }

		public async Task<IEnumerable<T>> SetAsync<T>()
        {
			if (!Points.Any())
				Points = await ReadDataAsync();

			PropertyInfo propertyInfo = GetType().GetProperties().FirstOrDefault(p => p.PropertyType == typeof(IEnumerable<T>));
			var set = propertyInfo?.GetValue(this);
			IEnumerable<T> result = (IEnumerable<T>)set;

			return result ?? Enumerable.Empty<T>();
		}

        public int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

		private async Task<IEnumerable<Point>> ReadDataAsync()
		{
			var productsJson = await File.ReadAllTextAsync(@"..\..\..\DataSource\products.json");
			var result = JsonSerializer.Deserialize<List<Point>>(productsJson);
			return result ?? Enumerable.Empty<Point>();
		}
	}
}
