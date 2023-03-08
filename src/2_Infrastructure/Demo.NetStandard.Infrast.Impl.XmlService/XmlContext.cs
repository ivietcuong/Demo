using Demo.NetStandard.Core.Data;
using Demo.NetStandard.Core.Entities;
using Demo.NetStandard.Core.Interfaces;
using Demo.NetStandard.Core.Services;
using Demo.NetStandard.Infrast.Impl.XmlService;

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
		private readonly IPathService _pathService;

		public IEnumerable<Point> Points { get; set; }

		public XmlContext(IXmlPathService pathService)
		{
			_pathService = pathService;
		}
		public async Task WriteXmlDataAsync()
		{
			var taskCompletionSource = new TaskCompletionSource<Task>();

			XmlSerializer serializer = new XmlSerializer(typeof(List<Point>));
			FileStream streamReader = File.Create(_pathService.GetPath());
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
			Stream reader = new FileStream(_pathService.GetPath(), FileMode.Open);

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
