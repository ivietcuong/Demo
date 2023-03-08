using CommunityToolkit.Mvvm.ComponentModel;

using Demo.NetStandard.Core.Entities;
using Demo.NetStandard.Core.Services;

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Net.Wpf.JsonPresenter.ViewModels
{
	public partial class JsonControlViewModel : ObservableObject
	{
		private readonly IPointService _pointService;

		[ObservableProperty]
		private ObservableCollection<Point> _points = new ObservableCollection<Point>();

		public JsonControlViewModel(IPointService pointService)
		{
			_pointService = pointService;
			InitializePoints(GetPoints());
		}

		private async Task GetPoints()
		{
			try
			{
				var points = await _pointService.GetPointListAsync();
				Points = new ObservableCollection<Point>(points.Select(p => new Point() { X = p.X, Y = -2 * Math.Pow(p.X, 3) + 3 * Math.Pow(p.X, 2) - 4 * p.X + 1 }));
				//Points = new ObservableCollection<Point>(points.Select(p => new Point() { X = p.X, Y = Math.Tan(p.X) }));
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

