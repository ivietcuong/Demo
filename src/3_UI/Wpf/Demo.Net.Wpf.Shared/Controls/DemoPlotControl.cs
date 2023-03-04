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
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Demo.Net.Wpf.Shared.Controls"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Demo.Net.Wpf.Shared.Controls;assembly=Demo.Net.Wpf.Shared.Controls"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:DemoPlotControl/>
    ///
    /// </summary>
    [TemplatePart(Name = "PART_PlotView", Type = typeof(PlotView))]
    public class DemoPlotControl : Control
    {
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(DemoPlotControl), new FrameworkPropertyMetadata(string.Empty, new PropertyChangedCallback(OnTitleChanged)));

        public static readonly DependencyProperty SubtitleProperty =
            DependencyProperty.Register("Subtitle", typeof(string), typeof(DemoPlotControl), new FrameworkPropertyMetadata(string.Empty, new PropertyChangedCallback(OnSubTitleChanged)));

        public static readonly DependencyProperty SeriesTitleProperty =
            DependencyProperty.Register("SeriesTitle", typeof(string), typeof(DemoPlotControl), new FrameworkPropertyMetadata(string.Empty, new PropertyChangedCallback(OnSeriesTitleChanged)));

        public static readonly DependencyProperty PointsProperty =
            DependencyProperty.Register("Points", typeof(List<DemoCore.Point>), typeof(DemoPlotControl), new PropertyMetadata(default, new PropertyChangedCallback(OnPointsChanged)));

        private PlotView _plotView = null!;
        private LineSeries _lineSeries = null!;

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public string Subtitle
        {
            get { return (string)GetValue(SubtitleProperty); }
            set { SetValue(SubtitleProperty, value); }
        }
        public string SeriesTitle
        {
            get { return (string)GetValue(SeriesTitleProperty); }
            set { SetValue(SeriesTitleProperty, value); }
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
                Subtitle = Subtitle,
            };

            _lineSeries = new LineSeries
            {
                Title = SeriesTitle
            };

            _lineSeries.Points.AddRange(Points.Select(p => new DataPoint(p.X, p.Y)));
            _plotView.Model.Series.Add(_lineSeries);
            _plotView.Model.InvalidatePlot(true);
        }

        private static void OnPointsChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            DemoPlotControl control = (DemoPlotControl)dependencyObject;

            if (control == null || control._plotView == null || e.NewValue == null)
                return;
        }

        private static void OnTitleChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            DemoPlotControl control = (DemoPlotControl)dependencyObject;

            if (control == null || control._plotView == null || e.NewValue == null)
                return;

            control._plotView.Model.Title = (string)e.NewValue;
        }

        private static void OnSubTitleChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            DemoPlotControl control = (DemoPlotControl)dependencyObject;

            if (control == null || control._plotView == null || e.NewValue == null)
                return;

            control._plotView.Model.Subtitle = (string)e.NewValue;
        }

        private static void OnSeriesTitleChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            DemoPlotControl control = (DemoPlotControl)dependencyObject;

            if (control == null || control._plotView == null || control._lineSeries == null || e.NewValue == null)
                return;

            control._lineSeries.Title = (string)e.NewValue;
        }
    }
}
