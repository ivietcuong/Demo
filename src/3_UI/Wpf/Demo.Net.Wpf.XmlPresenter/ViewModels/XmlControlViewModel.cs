using CommunityToolkit.Mvvm.ComponentModel;

using Demo.Net.Wpf.Shared;
using Demo.NetStandard.Core.Entities;
using Demo.NetStandard.Core.Services;

using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Net.Wpf.XmlPresenter.ViewModels
{
	public partial class XmlControlViewModel : ObservableObject
	{
		private readonly IPointService _pointService;
		private readonly ILogger<XmlControlViewModel> _logger;

		[ObservableProperty]
		private List<Point> _points = new List<Point>();

		public XmlControlViewModel(IPointService pointService, ILogger<XmlControlViewModel> logger)
		{
			_logger = logger;
			_pointService = pointService;
			GetPoints().InitializeData(_logger);
		}
		private async Task GetPoints()
		{
			try
			{
				var points = await _pointService.GetPointListAsync();
				//Points = new List<Point>(points.Select(p => new Point() { X = p.X, Y = 3 * Math.Pow(p.X, 2) - 4 * p.X + 1 }));
				Points = (from point in points
						  select new Point() { X = point.X - points.Count() / 2, Y = Math.Pow(point.X - points.Count() / 2, 2) + 3 * point.X - points.Count() / 2 + 2 }).ToList();
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.Message);
				throw;
			}
		}
	}
}
