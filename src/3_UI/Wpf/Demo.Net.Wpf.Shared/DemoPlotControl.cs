using DemoCore = Demo.NetStandard.Core.Entities;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Wpf;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Collections;

namespace Demo.Net.Wpf.Shared
{
	[TemplatePart(Name = "PART_PlotView", Type = typeof(PlotView))]
	public class DemoPlotControl : Control
	{
		public static readonly DependencyProperty ItemsSourceProperty =
			DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(DemoPlotControl), new PropertyMetadata(null));

		public static readonly DependencyProperty TitleProperty =
			DependencyProperty.Register("Title", typeof(string), typeof(DemoPlotControl), new FrameworkPropertyMetadata(string.Empty, new PropertyChangedCallback(OnTitleChanged)));

		public static readonly DependencyProperty SubtitleProperty =
			DependencyProperty.Register("Subtitle", typeof(string), typeof(DemoPlotControl), new FrameworkPropertyMetadata(string.Empty, new PropertyChangedCallback(OnSubTitleChanged)));


		private PlotView _plotView = null!;
		private LineSeries _lineSeries = null!;

		public string? Title
		{
			get { return (string)GetValue(TitleProperty); }
			set { SetValue(TitleProperty, value); }
		}
		public string? Subtitle
		{
			get { return (string)GetValue(SubtitleProperty); }
			set { SetValue(SubtitleProperty, value); }
		}
		public IEnumerable ItemsSource
		{
			get { return (IEnumerable)GetValue(ItemsSourceProperty); }
			set { SetValue(ItemsSourceProperty, value); }
		}

		static DemoPlotControl()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(DemoPlotControl), new FrameworkPropertyMetadata(typeof(DemoPlotControl)));
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			if (Template == null)
				return;

			_plotView = (PlotView)Template.FindName("PART_PlotView", this);

			_plotView.Model = new PlotModel
			{
				Title = Title,
				Subtitle = Subtitle,
				TitleFont = "Sitka Display Semibold",
				SubtitleFont = "Sitka Display Semibold"
			};

			_lineSeries = new LineSeries();

			if (ItemsSource != null)
				_lineSeries.Points.AddRange(ItemsSource.Cast<DemoCore.Point>().Select(p => new DataPoint(p.X, p.Y)));

			_plotView.Model.Series.Add(_lineSeries);
			_plotView.Model.InvalidatePlot(true);
		}

		private static void OnPointsChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
		{
			var control = (DemoPlotControl)dependencyObject;

			if (control == null || control._plotView == null || e.NewValue == null)
				return;
		}

		private static void OnTitleChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
		{
			var control = (DemoPlotControl)dependencyObject;

			if (control == null || control._plotView == null || e.NewValue == null)
				return;
		}

		private static void OnSubTitleChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
		{
			var control = (DemoPlotControl)dependencyObject;

			if (control == null || control._plotView == null || e.NewValue == null)
				return;
		}

		private static void OnSeriesTitleChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
		{
			var control = (DemoPlotControl)dependencyObject;

			if (control == null || control._plotView == null || control._lineSeries == null || e.NewValue == null)
				return;
		}
	}
}
