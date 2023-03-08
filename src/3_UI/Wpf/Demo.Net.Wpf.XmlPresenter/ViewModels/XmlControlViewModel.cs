using CommunityToolkit.Mvvm.ComponentModel;

using Demo.NetStandard.Core.Entities;
using Demo.NetStandard.Core.Services;

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

		[ObservableProperty]
		private List<Point> _points = new List<Point>();

		public XmlControlViewModel(IPointService pointService)
		{
			_pointService = pointService;
			InitializePoints(GetPoints());
		}
		private async Task GetPoints()
		{
			try
			{
				var points = await _pointService.GetPointListAsync();
				Points = new List<Point>(points.Select(p => new Point() { X = p.X, Y = 3 * Math.Pow(p.X, 2) - 4 * p.X + 1 }));
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.Message);
				throw;
			}
		}

		private async void InitializePoints(Task task)
		{
			try
			{
				await task;
			}
			catch (Exception)
			{

				throw;
			}
		}
	}
}
