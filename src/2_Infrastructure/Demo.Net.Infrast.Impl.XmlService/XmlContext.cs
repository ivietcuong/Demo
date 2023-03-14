using Demo.Net.Core.DataSource;
using Demo.Net.Core.Entities;
using Demo.Net.Core.Interfaces;
using Demo.Net.Core.Services;

using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Demo.Net.Infrast.Impl.XmlService
{
	public class XmlContext : IUnitOfWork
	{
		private readonly ILogger _logger;
		private readonly IPathService _pathService;

		public IEnumerable<Point> Points { get; set; }

		public XmlContext(IXmlPathService pathService, ILogger<XmlContext> logger = null)
		{
			_logger = logger;
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

		public async Task<IEnumerable<T>> SetAsync<T>() where T : class
		{
			try
			{
				if (Points == null || !Points.Any())
					Points = await ReadXmlDataAsync();

				PropertyInfo propertyInfo = GetType().GetProperties().FirstOrDefault(p => p.PropertyType == typeof(IEnumerable<T>));
				var set = propertyInfo?.GetValue(this);
				IEnumerable<T> result = (IEnumerable<T>)set;

				_logger.LogTrace($"{nameof(SetAsync)} {set.GetType().Name} {result.Count()}");

				return result ?? Enumerable.Empty<T>();
			}
			catch (Exception e)
			{
				_logger.LogError(e.Message);
				throw;
			}
		}

		public int SaveChanges(bool acceptAllChangesOnSuccess)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<Point>> ReadXmlDataAsync()
		{
			try
			{
				var taskCompletionSource = new TaskCompletionSource<IEnumerable<Point>>();

				XmlSerializer serializer = new XmlSerializer(typeof(List<Point>));
				Stream reader = new FileStream(_pathService.GetPath(), FileMode.Open);

				var result = (IEnumerable<Point>)serializer.Deserialize(reader);
				reader.Close();

				taskCompletionSource.SetResult(result ?? Enumerable.Empty<Point>());

				_logger.LogTrace($"{nameof(ReadXmlDataAsync)} {_pathService.GetPath()} {result.Count()}");

				return await taskCompletionSource.Task;
			}
			catch (Exception e)
			{
				_logger.LogError(e.Message);
				throw;
			}
		}

		public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			throw new NotImplementedException();
		}
	}
}
