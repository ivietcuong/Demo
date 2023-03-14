using Demo.NetStandard.Core.DataSource;

using OxyPlot;
using OxyPlot.Legends;
using OxyPlot.Series;

namespace Demo.NET.Maui.UIApp.Views;

public partial class HomeView : ContentView
{
	public HomeView()
	{
		InitializeComponent();
		PlotView.Model = new PlotModel
		{
			Title = "SkiaShart",
			Subtitle = "Maui",
			TitleFont = "Sitka Display Semibold",
			SubtitleFont = "Sitka Display Semibold"
		};

		var legend = new Legend
		{
			LegendBorder = OxyColors.Black,
			LegendPosition = LegendPosition.RightTop,
			LegendOrientation = LegendOrientation.Vertical,
			LegendBackground = OxyColor.FromAColor(200, OxyColors.White)
		};

		PlotView.Model.Legends.Add(legend);

		var points = Data.Points.Cast<NetStandard.Core.Entities.Point>().Select(p => new DataPoint(p.X, p.Y));
		var lineseries = new LineSeries();
		lineseries.Points.AddRange(points);
		PlotView.Model.Series.Add(lineseries);
		PlotView.Model.InvalidatePlot(true);
	}

}