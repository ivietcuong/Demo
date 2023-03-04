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
            var tmp = new PlotModel { Title = "Simple example", Subtitle = "using OxyPlot" };
            var series = new LineSeries { Title = "Series" };
            Data.Points.ForEach(p => { series.Points.Add(new DataPoint(p.X, p.Y)); });
            tmp.Series.Add(series);
            Model = tmp;

            Points = new List<Point>(Data.Points);
        }
    }
}
