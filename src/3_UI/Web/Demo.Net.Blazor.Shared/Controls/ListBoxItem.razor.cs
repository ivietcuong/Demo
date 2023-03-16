using Microsoft.AspNetCore.Components;

namespace Demo.Net.Blazor.Shared.Controls
{
    public partial class ListBoxItem
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}
