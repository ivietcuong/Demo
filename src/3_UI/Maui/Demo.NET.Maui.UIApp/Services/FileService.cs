using Microsoft.Extensions.Logging;

namespace Demo.Net.Maui.UIApp.Services
{
	public class FileService : IFileService
	{
		private readonly IFileSystem _filesystem;
		private readonly ILogger<FileService> _logger;

		public FileService(IFileSystem filesystem, ILogger<FileService> logger)
		{
			_filesystem = filesystem;
			_logger = logger;
		}

		public async Task CopyFromAppPackageFileToAppDataDirectory()
		{
			try
			{
				if (File.Exists(Path.Combine(_filesystem.AppDataDirectory, "points.db")))
					return;

				using (Stream stream = await _filesystem.OpenAppPackageFileAsync("points.db"))
				{
					using (MemoryStream memoryStream = new MemoryStream())
					{
						stream.CopyTo(memoryStream);

						await File.WriteAllBytesAsync(Path.Combine(_filesystem.AppDataDirectory, "points.db"), memoryStream.ToArray());
					}
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message, ex);
			}
		}
	}
}
