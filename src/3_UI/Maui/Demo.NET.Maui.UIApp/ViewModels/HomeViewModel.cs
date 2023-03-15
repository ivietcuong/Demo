using CommunityToolkit.Mvvm.ComponentModel;

using Demo.Net.Maui.UIApp.Services;

using Microsoft.Extensions.Logging;

using System.Collections.ObjectModel;

using Demo.Net.Maui.Shared;

using Point = Demo.NetStandard.Core.Entities.Point;

namespace Demo.Net.Maui.UIApp.ViewModels
{
	public partial class HomeViewModel : ObservableObject
	{
		private readonly IFileService _fileservice;
		[ObservableProperty]
		private ObservableCollection<Point> _points;

		public HomeViewModel(IFileService fileservice, ILogger<HomeViewModel> logger)
		{
			_fileservice = fileservice;
			GetPoints().InitializeData(logger);
		}

		private async Task GetPoints()
		{
			await _fileservice.CopyFromAppPackageFileToAppDataDirectory();
			Points = new ObservableCollection<Point>(NetStandard.Core.DataSource.Data.Points);
		}
	}
}
