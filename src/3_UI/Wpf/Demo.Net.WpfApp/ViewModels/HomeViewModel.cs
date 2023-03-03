using CommunityToolkit.Mvvm.ComponentModel;

using Demo.NetStandard.Core.Data;

using OxyPlot;
using OxyPlot.Series;

namespace Demo.Net.WpfApp.ViewModels
{
	public partial class HomeViewModel : ObservableObject
	{
        [ObservableProperty]
        private PlotModel? _model;

        public HomeViewModel()
        {
            var tmp = new PlotModel { Title = "Simple example", Subtitle = "using OxyPlot" };
            var series = new LineSeries { Title = "Series"};
            Data.Points.ForEach(p => { series.Points.Add(new DataPoint(p.X, p.Y)); });
            tmp.Series.Add(series);
            Model = tmp;
        }
    }
}
