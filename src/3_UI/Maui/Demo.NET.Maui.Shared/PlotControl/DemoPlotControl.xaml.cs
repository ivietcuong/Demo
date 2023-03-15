using OxyPlot.Legends;
using OxyPlot;
using OxyPlot.Maui.Skia;

using System.Collections;
using OxyPlot.Series;
using Point = Demo.NetStandard.Core.Entities.Point;
using OxyPlot.Axes;

namespace Demo.Net.Maui.Shared;

public partial class DemoPlotControl : ContentView
{
	private const string PartPlotView = "PART_PlotView";
    private const string LinearAxisLeft = "LinearAxisLeft";
    private const string LinearAxisBottom = "LinearAxisBottom";

    private PlotView _plotView = null!;
	private LineSeries _lineSeries = null!;

	public string Title
	{
		get { return (string)GetValue(TitleProperty); }
		set { SetValue(TitleProperty, value); }
	}
	public static readonly BindableProperty TitleProperty = 
		BindableProperty.Create(nameof(Title), typeof(string), typeof(DemoPlotControl), string.Empty, BindingMode.TwoWay, null, propertyChanged: OnTitleSourceChanged);

	public string Subtitle
	{
		get { return (string)GetValue(SubTitleProperty); }
		set { SetValue(SubTitleProperty, value); }
	}
	public static readonly BindableProperty SubTitleProperty = 
		BindableProperty.Create(nameof(Subtitle), typeof(string), typeof(DemoPlotControl), string.Empty, BindingMode.TwoWay);

	public string LineTitle
	{
		get { return (string)GetValue(LineTitleProperty); }
		set { SetValue(LineTitleProperty, value); }
	}
	public static readonly BindableProperty LineTitleProperty = 
		BindableProperty.Create(nameof(LineTitle), typeof(string), typeof(DemoPlotControl), string.Empty, BindingMode.TwoWay);

	public IEnumerable ItemsSource
	{
		get { return (IEnumerable)GetValue(ItemsSourceProperty); }
		set { SetValue(ItemsSourceProperty, value); }
	}
	public static readonly BindableProperty ItemsSourceProperty = 
		BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(DemoPlotControl), default, BindingMode.TwoWay, null, propertyChanged: OnItemSourceChanged);

	public DemoPlotControl()
	{
		InitializeComponent();
		ControlTemplate = (ControlTemplate)Resources["DemoPlotControlTemplate"];
	}

	private void UpdateControl(DemoPlotControl demoPlotControl)
	{
		demoPlotControl._plotView.Model.Series.Clear();
		demoPlotControl._lineSeries = new LineSeries() { Title = demoPlotControl.LineTitle };

		if (demoPlotControl.ItemsSource != null)
			demoPlotControl._lineSeries.Points.AddRange(demoPlotControl.ItemsSource.Cast<Point>().Select(p => new DataPoint(p.X, p.Y)));

		demoPlotControl._plotView.Model.Series.Add(demoPlotControl._lineSeries);

		if (!demoPlotControl._lineSeries.Points.Any())
			return;

        var m = Math.Abs(demoPlotControl._lineSeries.Points.MaxBy(p => p.Y).Y) > Math.Abs(demoPlotControl._lineSeries.Points.MinBy(p => p.Y).Y) ?
                    Math.Abs(demoPlotControl._lineSeries.Points.MaxBy(p => p.Y).Y) : Math.Abs(demoPlotControl._lineSeries.Points.MinBy(p => p.Y).Y);

        var linearaxisleft = demoPlotControl._plotView.Model.Axes.FirstOrDefault(a => a.Key.Equals(LinearAxisLeft));
        if (linearaxisleft != null)
        {
            linearaxisleft.Maximum = m;
            linearaxisleft.Minimum = -m;
        }

        demoPlotControl._plotView.Model.InvalidatePlot(true);
	}

	private static void OnItemSourceChanged(BindableObject bindable, object oldValue, object newValue)
	{
		var control = (DemoPlotControl)bindable;

		if (control == null || control._plotView == null || newValue == null)
			return;

		control?.UpdateControl(control);
	}

	private static void OnTitleSourceChanged(BindableObject bindable, object oldValue, object newValue)
	{
		var control = (DemoPlotControl)bindable;

		if (control == null || control._plotView == null || newValue == null)
			return;

		control._plotView.Model.Title = newValue.ToString();
		control._plotView.Model.InvalidatePlot(true);
	}

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		_plotView = (PlotView)GetTemplateChild(PartPlotView);

		_plotView.Model = new PlotModel
		{
			Title = Title,
			Subtitle = Subtitle,
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
		_plotView.Model.Legends.Add(legend);

        var linearAxisLeft = new LinearAxis
        {
            Key = LinearAxisLeft,
            PositionAtZeroCrossing = true,
            TickStyle = TickStyle.Crossing,
            AxislineStyle = LineStyle.Solid
        };
        _plotView.Model.Axes.Add(linearAxisLeft);

        var linearAxisBottom = new LinearAxis
        {
            Maximum = 1900,
            Minimum = -1900,
            Key = LinearAxisBottom,
            PositionAtZeroCrossing = true,
            Position = AxisPosition.Bottom,
            TickStyle = TickStyle.Crossing,
            AxislineStyle = LineStyle.Solid
        };
        _plotView.Model.Axes.Add(linearAxisBottom);


        UpdateControl(this);
	}

}