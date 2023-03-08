using CommunityToolkit.Mvvm.ComponentModel;

using Demo.Net.Wpf.Shared;
using Demo.NetStandard.Core.Entities;
using Demo.NetStandard.Core.Services;

using Microsoft.Extensions.Logging;

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Net.Wpf.JsonPresenter.ViewModels
{
	public partial class JsonControlViewModel : ObservableObject
	{
		private readonly ILogger<JsonControlViewModel> _logger;
		private readonly IPointService _pointService;

		[ObservableProperty]
		private ObservableCollection<Point> _points = new ObservableCollection<Point>();

		public JsonControlViewModel(IPointService pointService, ILogger<JsonControlViewModel> logger)
		{
			_logger = logger;
			_pointService = pointService;

			GetPoints().InitializeData(_logger);
		}

		private async Task GetPoints()
		{
			try
			{
				_logger.LogInformation($"{nameof(GetPoints)}");	
				var points = await _pointService.GetPointListAsync();
				Points = new ObservableCollection<Point>(points.Select(p => new Point() { X = p.X, Y = -2 * Math.Pow(p.X, 3) + 3 * Math.Pow(p.X, 2) - 4 * p.X + 1 }));
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.Message);
				throw;
			}
		}
	}
}

