using Demo.Net.Maui.UIApp.ViewModels;
using Demo.Net.Core.DataSource;

using OxyPlot;
using OxyPlot.Legends;
using OxyPlot.Series;

namespace Demo.Net.Maui.UIApp.Views;

public partial class HomeView : ContentView
{
	public HomeView()
	{
		InitializeComponent();
		PlotView.Model = new PlotModel
		{
			Title = "SkiaSharp",
			Subtitle = "Maui"
		};

		var legend = new Legend
		{
			LegendBorder = OxyColors.Black,
			LegendPosition = LegendPosition.RightTop,
			LegendOrientation = LegendOrientation.Vertical,
			LegendBackground = OxyColor.FromAColor(200, OxyColors.White)
		};

		PlotView.Model.Legends.Add(legend);

		var points = Data.Points.Cast<Net.Core.Entities.Point>().Select(p => new DataPoint(p.X, p.Y));
		var lineseries = new LineSeries() { Title = "LineSeries" };
		lineseries.Points.AddRange(points);
		PlotView.Model.Series.Add(lineseries);
		PlotView.Model.InvalidatePlot(true);
	}

	public HomeView(HomeViewModel viewModel)
		: this()
	{
		BindingContext = viewModel;
	}

}