using Demo.NetStandard.Core.Entities;
using Demo.NetStandard.Core.Interfaces;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Demo.NetStandard.Infrast.XmlService.Impl
{
	public class XmlContext : IUnitOfWork
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

		public async Task<IEnumerable<Point>> ReadDataAsync()
		{
			var taskCompletionSource = new TaskCompletionSource<IEnumerable<Point>>();

			XmlSerializer serializer = new XmlSerializer(typeof(List<Point>));
			Stream reader = new FileStream(@"..\..\..\DataSource\products.xml", FileMode.Open);
			var result = (IEnumerable<Point>)serializer.Deserialize(reader);
			reader.Close();

			taskCompletionSource.SetResult(result ?? Enumerable.Empty<Point>());
			return await taskCompletionSource.Task;
		}

		public async Task<IEnumerable<Point>> WriteDataAsync()
		{
			var taskCompletionSource = new TaskCompletionSource<IEnumerable<Point>>();

			XmlSerializer serializer = new XmlSerializer(typeof(List<Point>));
			Stream reader = new FileStream(@"..\..\..\DataSource\products.xml", FileMode.Open);
			var result = (IEnumerable<Point>)serializer.Deserialize(reader);
			reader.Close();

			taskCompletionSource.SetResult(result ?? Enumerable.Empty<Point>());
			return await taskCompletionSource.Task;
		}
	}
}
