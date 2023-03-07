using Demo.NetStandard.Core.Data;
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
				Points = await ReadJsonDataAsync();

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

		public async Task<IEnumerable<Point>> ReadJsonDataAsync()
		{
			var jsonPoints = await File.ReadAllTextAsync(@"..\..\..\..\..\..\DataSource\points.json");
			var result = JsonSerializer.Deserialize<List<Point>>(jsonPoints);
			return result ?? Enumerable.Empty<Point>();
		}

		/// <summary>
		/// Serializing to UTF-8 byte array is about 5-10% faster than the string-based methods. The difference is because
		/// the bytes (as UTF-8) don't need to be converted to strings (UTF-16). But you can try it yourself out.
		/// </summary>
		/// <returns></returns>
		public async Task WriteJsonDataAsync()
		{
			// to do the logging
			using FileStream filestream = File.Create(@"..\..\..\..\..\..\DataSource\points.json");
			await JsonSerializer.SerializeAsync(filestream, Data.Points);
			await filestream.DisposeAsync();
		}
	}
}
