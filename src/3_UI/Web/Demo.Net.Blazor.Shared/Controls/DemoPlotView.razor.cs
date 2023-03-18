using Demo.NetStandard.Core.Entities;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Demo.Net.Blazor.Shared.Controls
{
    public partial class DemoPlotView : IAsyncDisposable
    {
        private IJSObjectReference? _module;

        [Parameter, AllowNull]
        public IJSRuntime? JSRuntime
        {
            get; set;
        }

        [Parameter, AllowNull]
        public IEnumerable<Point>? Points
        {
            get; set;
        }

        public DemoPlotView()
        {
            Points = new List<Point>();
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

                var name = GetType().Assembly.GetName().Name;
                _module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", $"./_content/{GetType().Assembly.GetName().Name}/demoplot.js");

                await _module.InvokeVoidAsync("ShowXY", Points?.Select(p => p.X), Points?.Select(p => p.Y));
            }
            catch (Exception e)
            {
                if (_module != null)
                    await _module.DisposeAsync();
                Debug.WriteLine(e.Message);
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_module != null)
                await _module.DisposeAsync();
        }
    }
}
