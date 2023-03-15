using Demo.Net.Maui.Shared.ViewModels;
using Demo.Net.Maui.UIApp.ViewModels;
using Demo.NetStandard.Core.DataSource;

using OxyPlot;
using OxyPlot.Legends;
using OxyPlot.Series;

namespace Demo.Net.Maui.UIApp.Views;

public partial class HomeView : ContentView, IWorkspace
{
	public HomeView()
	{
		InitializeComponent();
		//PlotView.Model = new PlotModel
		//{
		//	Title = "SkiaSharp",
		//	Subtitle = "Maui"
		//};

		//var legend = new Legend
		//{
		//	LegendBorder = OxyColors.Black,
		//	LegendPosition = LegendPosition.RightTop,
		//	LegendOrientation = LegendOrientation.Vertical,
		//	LegendBackground = OxyColor.FromAColor(200, OxyColors.White)
		//};

		//PlotView.Model.Legends.Add(legend);

		//var points = Data.Points.Select(p => new DataPoint(p.X, p.Y));
		//var lineseries = new LineSeries() { Title = "LineSeries" };
		//lineseries.Points.AddRange(points);
		//PlotView.Model.Series.Add(lineseries);
		//PlotView.Model.InvalidatePlot(true);
	}

	public HomeView(HomeViewModel viewModel)
		: this()
	{
		BindingContext = viewModel;
	}

	public string Icon { get; set; }
	public string Description { get; set; }
}