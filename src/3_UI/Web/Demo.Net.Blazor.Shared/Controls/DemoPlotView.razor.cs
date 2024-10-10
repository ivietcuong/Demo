using Demo.NetStandard.Core.Entities;
using Demo.NetStandard.Core.Extentions;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Demo.Net.Blazor.Shared.Controls
{
    public partial class DemoPlotView : IAsyncDisposable
    {
        private IEnumerable<Point>? _points;
        private IJSObjectReference? _jsObjectReference;

        [Parameter, AllowNull]
        public IJSRuntime? JsRuntime
        {
            get; set;
        }

        #pragma warning disable BL0007
        [Parameter, AllowNull]
        public IEnumerable<Point>? Points
        {
            get => _points;
            set
            {
                if (value == null || !value.Any())
                    return;

                _points = value;

                RefreshPlot().Run();
            }
        }

        public DemoPlotView()
        {
            Points = new List<Point>();
        }

        private async Task RefreshPlot()
        {
            if (JsRuntime != null && _jsObjectReference != null)
                await _jsObjectReference.InvokeVoidAsync("ShowXY", Points?.Select(p => p.X), Points?.Select(p => p.Y));

        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (!firstRender)
                return;

            try
            {
                if (JsRuntime == null)
                    return;

                _jsObjectReference = await JsRuntime.InvokeAsync<IJSObjectReference>("import", $"./_content/{GetType().Assembly.GetName().Name}/demoplot.js");

                await RefreshPlot();
            }
            catch (Exception e)
            {
                if (_jsObjectReference != null)
                    await _jsObjectReference.DisposeAsync();

                Debug.WriteLine(e.Message);
            }
        }

        async ValueTask IAsyncDisposable.DisposeAsync()
        {
            try
            {
                GC.SuppressFinalize(this);
                if (_jsObjectReference != null)
                    await _jsObjectReference.DisposeAsync();
            }
            catch (JSDisconnectedException jsexc)
            {
                Debug.WriteLine($"{jsexc.Message}");
            }
            catch (TaskCanceledException taskexc)
            {
                Debug.WriteLine(taskexc.Message);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc.Message);
            }

        }
    }
}
