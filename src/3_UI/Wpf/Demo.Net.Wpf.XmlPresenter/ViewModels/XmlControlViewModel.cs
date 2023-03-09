using CommunityToolkit.Mvvm.ComponentModel;

using Demo.Net.Wpf.Shared;
using Demo.NetStandard.Core.Entities;
using Demo.NetStandard.Core.Services;

using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Net.Wpf.XmlPresenter.ViewModels
{
	public partial class XmlControlViewModel : ObservableObject
	{
		private readonly ILogger _logger;
		private readonly IPointService _pointService;

		[ObservableProperty]
		private IMathService? _selectedMathSerice;

		[ObservableProperty]
		private List<Point> _points = new();

		[ObservableProperty]
		private List<IMathService> _mathServices = new();

		public XmlControlViewModel(IPointService pointService, IEnumerable<IMathService> mathservices, ILogger<XmlControlViewModel> logger)
		{
			_logger = logger;
			_pointService = pointService;
			MathServices = mathservices.ToList();

			GetPoints().InitializeData(_logger);
		}

		private async Task GetPoints()
		{
			try
			{
				var points = await _pointService.GetPointListAsync();
				var meanvalue = points.Count() / 2;
				Points = new List<Point>(points.Select(p => new Point() { X = p.X - meanvalue, Y = p.Y }));
				_logger.LogTrace($"{nameof(GetPoints)} {Points.Count}");
			}
			catch (Exception e)
			{
				_logger.LogError(e.Message);
				throw;
			}
		}

		partial void OnSelectedMathSericeChanged(IMathService? value)
		{
			if (SelectedMathSerice != null)
				Points = new List<Point>(SelectedMathSerice.Calculate(Points, 2, 3, 4));

			OnPropertyChanged(nameof(SelectedMathSerice.Name));
		}
	}
}
