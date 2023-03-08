
using Microsoft.Extensions.Logging;

using System;
using System.Threading.Tasks;

namespace Demo.Net.Wpf.Shared
{
	public static class DemoExtensions
	{
		public static async void InitializeData(this Task task, ILogger logger)
		{
			try
			{
				await task;
				logger.LogInformation($"{nameof(InitializeData)} - {task.Id}");
			}
			catch (Exception e)
			{
				logger.LogError(e, e.Message);
				throw;
			}
		}
	}
}
