using Demo.NetStandard.Core.Data;
using Demo.NetStandard.Core.Entities;
using Demo.NetStandard.Core.Interfaces;
using Demo.NetStandard.Core.Services;
using Demo.NetStandard.Infrast.Impl.JsonService;

using Microsoft.Extensions.Logging;

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
		private readonly IPathService _pathService;
		private readonly ILogger<JsonContext> _logger;

		public IEnumerable<Point> Points { get; set; }

		public JsonContext(IJsonPathService pathService, ILogger<JsonContext> logger = null)
		{
			_logger = logger;
			_pathService = pathService;
		}

		/// <summary>
		/// Serializing to UTF-8 byte array is about 5-10% faster than the string-based methods. The difference is because
		/// the bytes (as UTF-8) don't need to be converted to strings (UTF-16). But you can try it yourself out.
		/// </summary>
		/// <returns></returns>
		public async Task WriteJsonDataAsync()
		{
			// to do the logging
			using FileStream filestream = File.Create(_pathService.GetPath());
			await JsonSerializer.SerializeAsync(filestream, Data.Points);
			await filestream.DisposeAsync();
		}

		public async Task<IEnumerable<T>> SetAsync<T>()
		{
			try
			{

				if (Points == null || !Points.Any())
					Points = await ReadJsonDataAsync();

				PropertyInfo propertyInfo = GetType().GetProperties().FirstOrDefault(p => p.PropertyType == typeof(IEnumerable<T>));
				var set = propertyInfo?.GetValue(this);
				IEnumerable<T> result = (IEnumerable<T>)set;

				_logger.LogInformation($"{nameof(SetAsync)} {set.GetType().Name} {result.Count()}");

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

		public async Task<IEnumerable<Point>> ReadJsonDataAsync()
		{
			try
			{
				var jsonPoints = await File.ReadAllTextAsync(_pathService.GetPath());
				var result = JsonSerializer.Deserialize<List<Point>>(jsonPoints);

				_logger.LogInformation($"{nameof(ReadJsonDataAsync)} {_pathService.GetPath()} {result.Count()}");

				return result ?? Enumerable.Empty<Point>();
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
