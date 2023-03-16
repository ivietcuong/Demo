using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace Demo.Net.Blazor.Shared.Controls
{
    public partial class ListBox<TItem> : ComponentBase
    {

        [Parameter, AllowNull]
        public RenderFragment ChildContent
        {
            get; set;
        }

        [Parameter, AllowNull]
        public IEnumerable<TItem> ItemSource
        {
            get; set;
        }

        [Parameter]
        public RenderFragment<TItem>? ItemTemplate
        {
            get; set;
        }

        [Parameter]
        public EventCallback<TItem> SelectionChanged
        {
            get; set;
        }

        private Task OnClick(TItem item)
        {
            SelectionChanged.InvokeAsync(item);
            return Task.CompletedTask;
        }

    }
}
