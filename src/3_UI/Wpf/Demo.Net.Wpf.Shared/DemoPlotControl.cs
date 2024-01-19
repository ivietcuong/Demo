using DemoCore = Demo.NetStandard.Core.Entities;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Wpf;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Collections;
using OxyPlot.Legends;
using System;
using System.Diagnostics;
using OxyPlot.Axes;

namespace Demo.Net.Wpf.Shared
{
    [TemplatePart(Name = PartPlotView, Type = typeof(PlotView))]
    public class DemoPlotControl : Control
    {
        private const string PartPlotView = "PART_PlotView";
        private const string LinearAxisLeft = "LinearAxisLeft";
        private const string LinearAxisBottom = "LinearAxisBottom";


        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title), typeof(string), typeof(DemoPlotControl), new FrameworkPropertyMetadata(string.Empty, new PropertyChangedCallback(OnTitleChanged)));

        public static readonly DependencyProperty SubtitleProperty =
            DependencyProperty.Register(nameof(Subtitle), typeof(string), typeof(DemoPlotControl), new FrameworkPropertyMetadata(string.Empty, new PropertyChangedCallback(OnSubTitleChanged)));

        public static readonly DependencyProperty LineTitleProperty =
            DependencyProperty.Register(nameof(LineTitle), typeof(string), typeof(DemoPlotControl), new FrameworkPropertyMetadata(string.Empty, new PropertyChangedCallback(OnLineTitleChanged)));

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(nameof(ItemsSource), typeof(IEnumerable), typeof(DemoPlotControl), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnItemsSourceChanged)));

        private PlotView _plotView = null!;

        private LineSeries _lineSeries = null!;

        public string? Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
        public string? Subtitle
        {
            get => (string)GetValue(SubtitleProperty);
            set => SetValue(SubtitleProperty, value);
        }
        public string LineTitle
        {
            get => (string)GetValue(LineTitleProperty);
            set => SetValue(LineTitleProperty, value);
        }
        public IEnumerable ItemsSource
        {
            get => (IEnumerable)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        static DemoPlotControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DemoPlotControl), new FrameworkPropertyMetadata(typeof(DemoPlotControl)));
        }

        public override void OnApplyTemplate()
        {
            try
            {
                base.OnApplyTemplate();

                if (Template == null)
                    return;

                _plotView = (PlotView)Template.FindName(PartPlotView, this);

                _plotView.Model = new PlotModel
                {
                    Title = Title,
                    Subtitle = Subtitle,
                    TitleFont = "Sitka Display Semibold",
                    SubtitleFont = "Sitka Display Semibold",
                    PlotMargins = new OxyThickness(10, 10, 10, 10)
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
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }

        private void UpdateControl(DemoPlotControl control)
        {
            control._plotView.Model.ResetAllAxes();
            control._plotView.Model.Series.Clear();

            control._lineSeries = new LineSeries() { Title = control.LineTitle };

            if (control.ItemsSource != null)
                control._lineSeries.Points.AddRange(control.ItemsSource.Cast<DemoCore.Point>().Select(p => new DataPoint(p.X, p.Y)));

            control._plotView.Model.Series.Add(control._lineSeries);

            var m = Math.Abs(control._lineSeries.Points.MaxBy(p => p.Y).Y) > Math.Abs(control._lineSeries.Points.MinBy(p => p.Y).Y) ?
                    Math.Abs(control._lineSeries.Points.MaxBy(p => p.Y).Y) : Math.Abs(control._lineSeries.Points.MinBy(p => p.Y).Y);

            var linearaxisleft = control._plotView.Model.Axes.FirstOrDefault(a => a.Key.Equals(LinearAxisLeft));
            if (linearaxisleft != null)
            {
                linearaxisleft.Maximum = m;
                linearaxisleft.Minimum = -m;
            }

            control._plotView.Model.InvalidatePlot(true);

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

        private static void OnLineTitleChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var control = (DemoPlotControl)dependencyObject;

            if (control == null || control._plotView == null || e.NewValue == null)
                return;

            control._plotView.Model.Series[0].Title = e.NewValue.ToString();
            control._plotView.Model.InvalidatePlot(true);
        }

        private static void OnItemsSourceChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var control = (DemoPlotControl)dependencyObject;

            if (control == null || control._plotView == null || e.NewValue == null)
                return;

            control.UpdateControl(control);
        }


    }
}
