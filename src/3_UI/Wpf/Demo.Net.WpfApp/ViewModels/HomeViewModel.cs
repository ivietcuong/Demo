using CommunityToolkit.Mvvm.ComponentModel;

using Demo.NetStandard.Core.DataSource;
using Demo.NetStandard.Core.Entities;

using OxyPlot;

using System.Collections.ObjectModel;

namespace Demo.Net.WpfApp.ViewModels
{
	public partial class HomeViewModel : ObservableObject
    {
        [ObservableProperty]
        private PlotModel? _model;

        [ObservableProperty]
        public ObservableCollection<Point> _points = null!;

        public HomeViewModel()
        {
            Points = new ObservableCollection<Point>(Data.Points);
        }
    }
}
