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
        public IEnumerable<double>? X 
        {
            get; set; 
        }

        [Parameter, AllowNull]
        public IEnumerable<double>? Y 
        {
            get; set; 
        }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();

            try
            {
                await base.OnInitializedAsync();

                if (JSRuntime == null)
                    return;

                var name = GetType().Assembly.GetName().Name;
                _module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", $"./_content/{GetType().Assembly.GetName().Name}/demoplot.js");

                await _module.InvokeVoidAsync("ShowXY", X, Y);
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
