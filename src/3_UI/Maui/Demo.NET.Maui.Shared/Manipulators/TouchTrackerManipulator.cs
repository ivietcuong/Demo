using OxyPlot.Maui.Skia.Tracker;

using Timer = System.Timers.Timer;

namespace OxyPlot.Maui.Skia.Manipulators
{
    /// <summary>
    /// Provides a plot manipulator for tracker functionality.
    /// </summary>
    public class TouchTrackerManipulator : OxyPlot.TouchManipulator
    {
        /// <summary>
        /// The current series.
        /// </summary>
        private Series.Series? currentSeries;

        /// <summary>
        /// Initializes a new instance of the <see cref="TouchTrackerManipulator" /> class.
        /// </summary>
        /// <param name="plotView">The plot view.</param>
        public TouchTrackerManipulator(IPlotView plotView)
            : base(plotView)
        {
            Snap = true;
            PointsOnly = false;
            LockToInitialSeries = true;
            FiresDistance = 20.0;
            CheckDistanceBetweenPoints = false;

            // Note: the tracker manipulator should not handle pan or zoom
            SetHandledForPanOrZoom = false;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show tracker on points only (not interpolating).
        /// </summary>
        public bool PointsOnly { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to snap to the nearest point.
        /// </summary>
        public bool Snap { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to lock the tracker to the initial series.
        /// </summary>
        /// <value><c>true</c> if the tracker should be locked; otherwise, <c>false</c>.</value>
        public bool LockToInitialSeries { get; set; }

        /// <summary>
        /// Gets or sets the distance from the series at which the tracker fires.
        /// </summary>
        public double FiresDistance { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to check distance when showing tracker between data points.
        /// </summary>
        /// <remarks>This parameter is ignored if <see cref="PointsOnly"/> is equal to <c>False</c>.</remarks>
        public bool CheckDistanceBetweenPoints { get; set; }

        /// <summary>
        /// Occurs when a manipulation is complete.
        /// </summary>
        /// <param name="e">The <see cref="OxyPlot.OxyTouchEventArgs" /> instance containing the event data.</param>
        public override void Completed(OxyTouchEventArgs e)
        {
            base.Completed(e);

            currentSeries = null;
            // PlotView.HideTracker();
            if (PlotView.ActualModel != null)
            {
                PlotView.ActualModel.RaiseTrackerChanged(null);
            }
        }

        /// <summary>
        /// Occurs when a touch delta event is handled.
        /// </summary>
        /// <param name="e">The <see cref="OxyPlot.OxyTouchEventArgs" /> instance containing the event data.</param>
        public override void Delta(OxyTouchEventArgs e)
        {
            base.Delta(e);

            var v = (startPoint - e.Position);

            if (v != null && v.Value.Length > 10)
            {
                if (updateTrackerTimer != null && updateTrackerTimer.Enabled)
                {
                    updateTrackerTimer.Stop();
                    updateTrackerTimer = null;
                }

                // This is touch, we want to hide the tracker because the user is probably panning / zooming now
                PlotView.HideTracker();
            }
        }

        /// <summary>
        /// Occurs when an input device begins a manipulation on the plot.
        /// </summary>
        /// <param name="e">The <see cref="OxyPlot.OxyTouchEventArgs" /> instance containing the event data.</param>
        public override void Started(OxyTouchEventArgs e)
        {
            base.Started(e);
            startPoint = e.Position;

            currentSeries = PlotView.ActualModel != null ? PlotView.ActualModel.GetSeriesFromPoint(e.Position) : null;

            UpdateTrackerDelay(e.Position);
        }

        private ScreenPoint? startPoint;
        private Timer? updateTrackerTimer;

        private void UpdateTrackerDelay(ScreenPoint position)
        {
            if (updateTrackerTimer == null)
            {
                updateTrackerTimer = new Timer(100);
                updateTrackerTimer.AutoReset = false;
                updateTrackerTimer.Elapsed += (s, e) =>
                {
                    updateTrackerTimer = null;
                    ((BindableObject)PlotView ).Dispatcher.Dispatch(() => UpdateTracker(position));
                };
            }
            else
            {
                updateTrackerTimer.Stop();
            }
            updateTrackerTimer.Start();
        }

        /// <summary>
        /// Updates the tracker to the specified position.
        /// </summary>
        /// <param name="position">The position.</param>
        private void UpdateTracker(ScreenPoint position)
        {
            if (currentSeries == null || !LockToInitialSeries)
            {
                // get the nearest
                currentSeries = PlotView.ActualModel?.GetSeriesFromPoint(position, FiresDistance);
            }

            if (currentSeries == null)
            {
                if (!LockToInitialSeries)
                {
                    PlotView.HideTracker();
                }

                return;
            }

            var actualModel = PlotView.ActualModel;
            if (actualModel == null)
            {
                return;
            }

            if (!actualModel.PlotArea.Contains(position.X, position.Y))
            {
                return;
            }

            var result = TrackerHelper.GetNearestHit(
                currentSeries, position, Snap, PointsOnly, FiresDistance, CheckDistanceBetweenPoints);
            if (result != null)
            {
                result.PlotModel = actualModel;
                PlotView.ShowTracker(result);
                actualModel.RaiseTrackerChanged(result);
            }
        }
    }
}