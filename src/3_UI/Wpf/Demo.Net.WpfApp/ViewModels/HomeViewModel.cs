using CommunityToolkit.Mvvm.ComponentModel;

using Demo.NetStandard.Core.Data;
using Demo.NetStandard.Core.Entities;

using OxyPlot;
using OxyPlot.Series;

using System.Collections.Generic;

namespace Demo.Net.WpfApp.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        [ObservableProperty]
        private PlotModel? _model;

        [ObservableProperty]
        public List<Point> _points = null!;

        public HomeViewModel()
        {
            Points = new List<Point>(Data.Points);
        }
    }
}
