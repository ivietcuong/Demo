using Demo.Net.Blazor.Shared;

using Microsoft.AspNetCore.Components;

namespace Demo.Net.Blazor.App.Pages
{
    public partial class FetchData : LayoutComponentBase, IWorkspace
    {
        public Type Type { get => GetType(); }
        public string Name { get => nameof(FetchData); }
        public Dictionary<string, object?> Properties { get; }
    }
}
