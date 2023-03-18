using Demo.NetStandard.Core.Entities;

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
        public IJSRuntime? JSRuntime
        {
            get; set;
        }

        [Parameter, AllowNull]
        public IEnumerable<Point>? Points
        {
            get => _points;
            set
            {
                if (value == null || !value.Any())
                    return;

                _points = value;
                RefreshPlot();
            }
        }

        public DemoPlotView()
        {
            Points = new List<Point>();
        }
        
        private async void RefreshPlot()
        {
            if (JSRuntime != null && _jsObjectReference != null)
                await _jsObjectReference.InvokeVoidAsync("ShowXY", Points?.Select(p => p.X), Points?.Select(p => p.Y));

        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (!firstRender)
                return;
            try
            {
                if (JSRuntime == null)
                    return;

                _jsObjectReference = await JSRuntime.InvokeAsync<IJSObjectReference>("import", $"./_content/{GetType().Assembly.GetName().Name}/demoplot.js");

                 RefreshPlot();
            }
            catch (Exception e)
            {
                if (_jsObjectReference != null)
                    await _jsObjectReference.DisposeAsync();

                Debug.WriteLine(e.Message);
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_jsObjectReference != null)
                await _jsObjectReference.DisposeAsync();
        }
    }
}
