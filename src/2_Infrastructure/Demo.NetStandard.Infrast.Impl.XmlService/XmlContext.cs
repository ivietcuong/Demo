using Demo.NetStandard.Core.Data;
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


		public async Task WriteXmlDataAsync()
		{
			var taskCompletionSource = new TaskCompletionSource<Task>();

			XmlSerializer serializer = new XmlSerializer(typeof(List<Point>));
			FileStream streamReader = File.Create(@"..\..\..\..\..\..\DataSource\points.xml");
			serializer.Serialize(streamReader, Data.Points);
			streamReader.Close();

			taskCompletionSource.SetResult(Task.CompletedTask);
			await taskCompletionSource.Task;
		}

		public async Task<IEnumerable<T>> SetAsync<T>()
		{
			if (Points == null || !Points.Any())
				Points = await ReadXmlDataAsync();

			PropertyInfo propertyInfo = GetType().GetProperties().FirstOrDefault(p => p.PropertyType == typeof(IEnumerable<T>));
			var set = propertyInfo?.GetValue(this);
			IEnumerable<T> result = (IEnumerable<T>)set;

			return result ?? Enumerable.Empty<T>();
		}

		public int SaveChanges(bool acceptAllChangesOnSuccess)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<Point>> ReadXmlDataAsync()
		{
			var taskCompletionSource = new TaskCompletionSource<IEnumerable<Point>>();

			XmlSerializer serializer = new XmlSerializer(typeof(List<Point>));
#if TEST
			Stream reader = new FileStream(@"..\..\..\..\..\..\DataSource\points.xml", FileMode.Open);
#else
			Stream reader = new FileStream(@"..\..\..\..\..\..\DataSource\points.xml", FileMode.Open);
#endif

			var result = (IEnumerable<Point>)serializer.Deserialize(reader);
			reader.Close();

			taskCompletionSource.SetResult(result ?? Enumerable.Empty<Point>());
			return await taskCompletionSource.Task;
		}

		public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			throw new NotImplementedException();
		}
	}
}
