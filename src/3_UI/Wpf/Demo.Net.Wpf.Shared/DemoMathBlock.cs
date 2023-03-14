using OxyPlot.Wpf;
using OxyPlot;
using System.Windows.Controls;
using System.Windows;

namespace Demo.Net.Wpf.Shared
{
    /// <summary>
    /// Provides a control for displaying simple math.
    /// </summary>
    [TemplatePart(Name = PartCanvas, Type = typeof(Canvas))]
    public class DemoMathBlock : ContentControl
    {
        /// <summary>
        /// The canvas template part.
        /// </summary>
        private const string PartCanvas = "PART_Canvas";

        /// <summary>
        /// The canvas
        /// </summary>
        private Canvas canvas = null!;

        /// <summary>
        /// The render context
        /// </summary>
        private IRenderContext renderContext = null!;

        /// <summary>
        /// Initializes static members of the <see cref="DemoMathBlock" /> class.
        /// </summary>
        static DemoMathBlock()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DemoMathBlock), new FrameworkPropertyMetadata(typeof(DemoMathBlock)));
            ContentProperty.OverrideMetadata(typeof(DemoMathBlock), new FrameworkPropertyMetadata(typeof(DemoMathBlock), (s, e) => ((DemoMathBlock)s).ContentChanged()));
        }

        /// <summary>
        /// Handles changes in the Content property.
        /// </summary>
        private void ContentChanged()
        {
            UpdateContent();
        }

        /// <summary>
        /// Called to measure a control.
        /// </summary>
        /// <param name="constraint">The maximum size that the method can return.</param>
        /// <returns>The size of the control, up to the maximum specified by <paramref name="constraint" />.</returns>
        protected override Size MeasureOverride(Size constraint)
        {
            if (Content == null)
                return base.MeasureOverride(constraint);

            var text = Content.ToString();

            double fontWeight = FontWeight.ToOpenTypeWeight();
            string fontFamily = string.Empty;

            if (FontFamily != null)
                fontFamily = FontFamily.Source;

            var size = renderContext.MeasureMathText(text, fontFamily, FontSize, fontWeight);
            return new Size(size.Width + Padding.Left + Padding.Right, size.Height + Padding.Top + Padding.Bottom);
        }

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes call <see cref="M:System.Windows.FrameworkElement.ApplyTemplate" />.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (Template == null)
                return;

            //canvas = GetTemplateChild(PartCanvas) as Canvas;
            canvas = (Canvas)Template.FindName(PartCanvas, this);
            renderContext = new CanvasRenderContext(canvas);
            SizeChanged += HandleSizeChanged;
        }

        /// <summary>
        /// Handles changes in control size.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SizeChangedEventArgs" /> instance containing the event data.</param>
        private void HandleSizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateContent();
        }

        /// <summary>
        /// Updates the content.
        /// </summary>
        private void UpdateContent()
        {
            if (canvas == null)
            {
                return;
            }

            canvas.Children.Clear();
            if (Content == null)
            {
                return;
            }

            var text = Content.ToString();
            var horizontalAlignment = HorizontalContentAlignment.ToHorizontalAlignment();
            var verticalAlignment = VerticalContentAlignment.ToVerticalAlignment();
            double fontWeight = FontWeight.ToOpenTypeWeight();
            double x = Padding.Left;
            switch (horizontalAlignment)
            {
                case OxyPlot.HorizontalAlignment.Right:
                    x = ActualWidth - Padding.Right;
                    break;
                case OxyPlot.HorizontalAlignment.Center:
                    x = Padding.Left + (ActualWidth - Padding.Left - Padding.Right) * 0.5;
                    break;
            }

            double y = Padding.Top;
            switch (verticalAlignment)
            {
                case OxyPlot.VerticalAlignment.Bottom:
                    y = ActualHeight - Padding.Bottom;
                    break;
                case OxyPlot.VerticalAlignment.Middle:
                    y = Padding.Top + (ActualWidth - Padding.Bottom - Padding.Top) * 0.5;
                    break;
            }

            var p = new ScreenPoint(x, y);
            string fontFamily = string.Empty;
            if (FontFamily != null)
            {
                fontFamily = FontFamily.Source;
            }

            renderContext.DrawMathText(
                p,
                text,
                Foreground.ToOxyColor(),
                fontFamily,
                FontSize,
                fontWeight,
                0,
                horizontalAlignment,
                verticalAlignment);
        }
    }
}
