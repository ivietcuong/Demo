using DemoCore = Demo.NetStandard.Core.Entities;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Wpf;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

namespace Demo.Net.Wpf.Shared.Controls
{
    [TemplatePart(Name = "PART_PlotView", Type = typeof(PlotView))]
    public class DemoPlotControl : Control
    {
        public static readonly DependencyProperty PointsProperty =
            DependencyProperty.Register("Points", typeof(List<DemoCore.Point>), typeof(DemoPlotControl), new PropertyMetadata(null, new PropertyChangedCallback(OnPointsChanged)));

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
        public List<DemoCore.Point> Points
        {
            get { return (List<DemoCore.Point>)GetValue(PointsProperty); }
            set { SetValue(PointsProperty, value); }
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
                Subtitle = Subtitle
            };

            _lineSeries = new LineSeries();

            if (Points != null)
                _lineSeries.Points.AddRange(Points.Select(p => new DataPoint(p.X, p.Y)));

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
