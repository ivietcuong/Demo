using CommunityToolkit.Mvvm.ComponentModel;

using Demo.Net.Wpf.Shared;
using Demo.NetStandard.Core.Entities;
using Demo.NetStandard.Core.Services;

using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Net.Wpf.JsonPresenter.ViewModels
{
	public partial class JsonControlViewModel : ObservableObject
	{
		private readonly ILogger _logger;
		private readonly IPointService _pointService;

		[ObservableProperty]
		private IMathService? _selectedMathSerice;

		[ObservableProperty]
		private ObservableCollection<Point> _points = new();

		[ObservableProperty]
		private ObservableCollection<IMathService> _mathServices = new();

		public JsonControlViewModel(IPointService pointService, IEnumerable<IMathService> mathservices, ILogger<JsonControlViewModel> logger)
		{
			_logger = logger;
			_pointService = pointService;
			MathServices = new ObservableCollection<IMathService>(mathservices);

			GetPoints().InitializeData(_logger);
		}

		private async Task GetPoints()
		{
			try
			{
				var points = await _pointService.GetPointListAsync();
				var meanvalue = points.Count() / 2;
				Points = new ObservableCollection<Point>(points.Select(p => new Point() { X = p.X - meanvalue, Y = p.Y }));
				_logger.LogTrace($"{nameof(GetPoints)} {Points.Count}");
			}
			catch (Exception e)
			{
				_logger.LogError(e.Message);
				throw;
			}
		}
		/// <summary>
		/// https://learn.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/generators/observableproperty
		/// </summary>
		/// <param name="value"></param>
		partial void OnSelectedMathSericeChanged(IMathService? value)
		{
			if (SelectedMathSerice != null)
				Points = new ObservableCollection<Point>(SelectedMathSerice.Calculate(Points, 3, 4, 5));
		}
	}
}

