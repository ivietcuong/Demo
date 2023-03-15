using Microsoft.Extensions.Logging;

namespace Demo.Net.Maui.Shared
{
	public static class DemoTaskExtension
	{
		public static async void InitializeData(this Task task, ILogger logger)
		{
			try
			{
				await task;
				logger.LogTrace($"{nameof(InitializeData)} - {task.Id}");
			}
			catch (Exception e)
			{
				logger.LogError(e, e.Message);
				throw;
			}
		}
	}
}
