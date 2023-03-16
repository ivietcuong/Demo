using Demo.Net.Blazor.Shared;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using System.Diagnostics.CodeAnalysis;

namespace Demo.Net.Blazor.App.Pages
{
    public partial class Index : ComponentBase, IWorkspace
    {
        [Inject, AllowNull]
        public IJSRuntime? JSRuntime { get; set; }
        public Type Type { get => GetType(); }
        public string Name { get => nameof(Index); }
        public Dictionary<string, object?> Parameters { get; } = new Dictionary<string, object?>();

        public List<double> X = new List<double>(NetStandard.Core.DataSource.Data.Points.Select(p => p.X));
        public List<double> Y = new List<double>(NetStandard.Core.DataSource.Data.Points.Select(p => p.Y));
    }
}
