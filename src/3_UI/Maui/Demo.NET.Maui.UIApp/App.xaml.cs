namespace Demo.Net.Maui.UIApp
{
	public partial class App : Application
	{
		private readonly IFileSystem _filesystem;

		public App()
		{
			InitializeComponent();
		}

		public App(MainPage mainPage, IFileSystem filesystem)
			: this()
		{
			MainPage = mainPage;
			_filesystem = filesystem;

		}

		protected override async void OnStart()
		{
			base.OnStart();
			await CopyFromAppPackageFileToAppDataDirectory();
		}

		private async Task CopyFromAppPackageFileToAppDataDirectory()
		{
			try
			{
				using (Stream stream = await FileSystem.OpenAppPackageFileAsync("points.db"))
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
			}
		}
	}
}