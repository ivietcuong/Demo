using OxyPlot.Maui.Skia.Core;

namespace OxyPlot.Maui.Skia
{
    public abstract partial class PlotViewBase : BaseTemplatedView<Grid>, IPlotView
    {
        private int mainThreadId = 1;

        protected override void OnControlInitialized(Grid control)
        {
            grid = control;
            mainThreadId = Thread.CurrentThread.ManagedThreadId;
            ApplyTemplate();
            AddTouchEffect();
        }

        /// <summary>
        /// The grid.
        /// </summary>
        protected Grid? grid;

        /// <summary>
        /// The plot presenter.
        /// </summary>
        protected View? plotPresenter;

        /// <summary>
        /// The render context
        /// </summary>
        protected IRenderContext? renderContext;

        /// <summary>
        /// The model lock.
        /// </summary>
        private readonly object modelLock = new object();

        /// <summary>
        /// The current tracker.
        /// </summary>
        private View? currentTracker;

        /// <summary>
        /// The current tracker template.
        /// </summary>
        private ControlTemplate? currentTrackerTemplate;

        /// <summary>
        /// The default plot controller.
        /// </summary>
        private IPlotController? defaultController;

        /// <summary>
        /// Indicates whether the <see cref="PlotViewBase"/> was in the visual tree the last time <see cref="Render"/> was called.
        /// </summary>
        private bool isInVisualTree;

        /// <summary>
        /// The overlays.
        /// </summary>
        private AbsoluteLayout? overlays;

        /// <summary>
        /// The zoom control.
        /// </summary>
        private ContentView? zoomControl;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlotViewBase" /> class.
        /// </summary>
        protected PlotViewBase()
        {
            TrackerDefinitions = new ObservableCollection<TrackerDefinition>();

            DefaultTrackerTemplate = new ControlTemplate(() =>
            {
                var tc = new TrackerControl();
                tc.SetBinding(TrackerControl.PositionProperty, "Position");
                tc.SetBinding(TrackerControl.LineExtentsProperty, "PlotModel.PlotArea");
                tc.Content = TrackerControl.DefaultTrackerTemplateContentProvider();
                return tc;
            });
            LayoutChanged += OnLayoutUpdated;
        }

        /// <summary>
        /// Gets the actual PlotView controller.
        /// </summary>
        /// <value>The actual PlotView controller.</value>
        public IPlotController ActualController => Controller ?? (defaultController ??= new OxyPlot.PlotController());

        /// <inheritdoc/>
        IController IView.ActualController => ActualController;

        /// <summary>
        /// Gets the actual model.
        /// </summary>
        /// <value>The actual model.</value>
        public PlotModel? ActualModel { get; private set; }

        /// <inheritdoc/>
        Model? IView.ActualModel => ActualModel;

        /// <summary>
        /// Gets the coordinates of the client area of the view.
        /// </summary>
        public OxyRect ClientArea => new OxyRect(0, 0, Width, Height);

        /// <summary>
        /// Gets the tracker definitions.
        /// </summary>
        /// <value>The tracker definitions.</value>
        public ObservableCollection<TrackerDefinition> TrackerDefinitions { get; }

        /// <summary>
        /// Hides the tracker.
        /// </summary>
        public void HideTracker()
        {
            if (currentTracker != null)
            {
                overlays?.Children.Remove(currentTracker);
                currentTracker = null;
                currentTrackerTemplate = null;
            }
        }

        /// <summary>
        /// Hides the zoom rectangle.
        /// </summary>
        public void HideZoomRectangle()
        {
            if (zoomControl != null)
                zoomControl.IsVisible = false;
        }

        /// <summary>
        /// Invalidate the PlotView (not blocking the UI thread)
        /// </summary>
        /// <param name="updateData">The update Data.</param>
        public void InvalidatePlot(bool updateData = true)
        {
            if (ActualModel == null)
                return;

            lock (ActualModel.SyncRoot)
            {
                ((IPlotModel)ActualModel).Update(updateData);
            }

            BeginInvoke(Render);
        }

        private void ApplyTemplate()
        {
            if (grid == null)
            {
                return;
            }

            plotPresenter = CreatePlotPresenter();
            grid.Children.Add(plotPresenter);

            renderContext = CreateRenderContext();

            overlays = new AbsoluteLayout();
            grid.Children.Add(overlays);

            zoomControl = new ContentView();
            overlays.Children.Add(zoomControl);
        }

        /// <summary>
        /// Pans all axes.
        /// </summary>
        public void PanAllAxes(double deltaX, double deltaY)
        {
            if (ActualModel != null)
            {
                ActualModel.PanAllAxes(deltaX, deltaY);
            }

            InvalidatePlot(false);
        }

        /// <summary>
        /// Resets all axes.
        /// </summary>
        public void ResetAllAxes()
        {
            if (ActualModel != null)
            {
                ActualModel.ResetAllAxes();
            }

            InvalidatePlot(false);
        }

        /// <summary>
        /// Stores text on the clipboard.
        /// </summary>
        /// <param name="text">The text.</param>
        public void SetClipboardText(string text)
        {
            Clipboard.SetTextAsync(text);
        }

        /// <summary>
        /// Sets the cursor type.
        /// </summary>
        /// <param name="cursorType">The cursor type.</param>
        public void SetCursorType(CursorType cursorType)
        {
        }

        /// <summary>
        /// Shows the tracker.
        /// </summary>
        /// <param name="trackerHitResult">The tracker data.</param>
        public void ShowTracker(TrackerHitResult trackerHitResult)
        {
            if (trackerHitResult == null)
            {
                HideTracker();
                return;
            }

            var trackerTemplate = DefaultTrackerTemplate;
            if (trackerHitResult.Series != null && !string.IsNullOrEmpty(trackerHitResult.Series.TrackerKey))
            {
                var match = TrackerDefinitions.FirstOrDefault(t => t.TrackerKey == trackerHitResult.Series.TrackerKey);
                if (match != null)
                {
                    trackerTemplate = match.TrackerTemplate;
                }
            }

            if (trackerTemplate == null)
            {
                HideTracker();
                return;
            }

            if (!ReferenceEquals(trackerTemplate, currentTrackerTemplate))
            {
                HideTracker();

                var tracker = (ContentView)trackerTemplate.CreateContent();
                if (overlays != null)
                {
                    overlays.Children.Add(tracker);
                    AbsoluteLayout.SetLayoutBounds(tracker, new Rect(0, 0, 0, 0));
                    currentTracker = tracker;
                    currentTrackerTemplate = trackerTemplate;
                }
            }

            if (currentTracker != null)
                currentTracker.BindingContext = trackerHitResult;
        }

        /// <summary>
        /// Shows the zoom rectangle.
        /// </summary>
        /// <param name="r">The rectangle.</param>
        public void ShowZoomRectangle(OxyRect r)
        {
            if (zoomControl == null)
                return;

            zoomControl.WidthRequest = r.Width;
            zoomControl.HeightRequest = r.Height;

            AbsoluteLayout.SetLayoutBounds(zoomControl,
                new Rect(r.Left, r.Top, r.Width, r.Height));

            zoomControl.ControlTemplate = ZoomRectangleTemplate;
            zoomControl.IsVisible = true;
        }

        /// <summary>
        /// Zooms all axes.
        /// </summary>
        /// <param name="factor">The zoom factor.</param>
        public void ZoomAllAxes(double factor)
        {
            if (ActualModel != null)
                ActualModel.ZoomAllAxes(factor);

            InvalidatePlot(false);
        }

        /// <summary>
        /// Clears the background of the plot presenter.
        /// </summary>
        protected abstract void ClearBackground();

        /// <summary>
        /// Creates the plot presenter.
        /// </summary>
        /// <returns>The plot presenter.</returns>
        protected abstract View CreatePlotPresenter();

        /// <summary>
        /// Creates the render context.
        /// </summary>
        /// <returns>The render context.</returns>
        protected abstract IRenderContext CreateRenderContext();

        /// <summary>
        /// Called when the model is changed.
        /// </summary>
        protected void OnModelChanged()
        {
            lock (modelLock)
            {
                if (ActualModel != null)
                {
                    ((IPlotModel)ActualModel).AttachPlotView(null);
                    ActualModel = null;
                }

                if (Model != null)
                {
                    IPlotModel plotModel = Model;
                    var oldPlotView = Model.PlotView;
                    if (!ReferenceEquals(oldPlotView, null) &&
                        !ReferenceEquals(oldPlotView, this))
                    {
                        // This PlotModel is already in use by some other PlotView control.
                        plotModel.AttachPlotView(null);
                    }

                    plotModel.AttachPlotView(this);
                    ActualModel = Model;
                }
            }

            InvalidatePlot();
        }

        /// <summary>
        /// Renders the plot model to the plot presenter.
        /// </summary>
        protected void Render()
        {
            if (plotPresenter == null || renderContext == null || !(isInVisualTree = IsInVisualTree()))
                return;

            RenderOverride();
        }

        /// <summary>
        /// Renders the plot model to the plot presenter.
        /// </summary>
        protected virtual void RenderOverride()
        {
            var dpiScale = UpdateDpi();
            ClearBackground();

            HideTracker();

            if (ActualModel != null && plotPresenter != null)
            {
                // round width and height to full device pixels
                var width = (int)(plotPresenter.Width * dpiScale) / dpiScale;
                var height = (int)(plotPresenter.Height * dpiScale) / dpiScale;

                lock (ActualModel.SyncRoot)
                {
                    ((IPlotModel)ActualModel).Render(renderContext, new OxyRect(0, 0, width, height));
                }
            }
        }

        /// <summary>
        /// Updates the DPI scale of the render context.
        /// </summary>
        /// <returns>The DPI scale.</returns>
        protected virtual double UpdateDpi()
        {
            return DeviceDisplay.MainDisplayInfo.Density;
        }

        /// <summary>
        /// Called when the model is changed.
        /// </summary>
        //event data.</param>
        private static void ModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((PlotViewBase)bindable).OnModelChanged();
        }

        /// <summary>
        /// Invokes the specified action on the dispatcher, if necessary.
        /// </summary>
        /// <param name="action">The action.</param>
        private void BeginInvoke(Action action)
        {
            if (!CheckAccess())
                Dispatcher.Dispatch(action);
            else
                action();
        }

        /// <summary>
        /// Determines whether the calling thread is the main thread 
        /// </summary>
        /// <returns></returns>
        private bool CheckAccess()
        {
            return Thread.CurrentThread.ManagedThreadId == mainThreadId;
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="PlotViewBase"/> is connected to the visual tree.
        /// </summary>
        /// <returns><c>true</c> if the PlotViewBase is connected to the visual tree; <c>false</c> otherwise.</returns>
        private bool IsInVisualTree()
        {
            Microsoft.Maui.Controls.Element dpObject = this;
            while ((dpObject = dpObject.Parent) != null)
                if (dpObject is Page)
                    return true;

            return false;
        }

        /// <summary>
        /// This event fires every time Layout updates the layout of the trees associated with current Dispatcher.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void OnLayoutUpdated(object? sender, EventArgs e)
        {
            // if we were not in the visual tree the last time we tried to render but are now, we have to render
            if (!isInVisualTree && IsInVisualTree())
                Render();
        }
    }
}