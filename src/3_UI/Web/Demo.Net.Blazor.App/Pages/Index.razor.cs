using Demo.Net.Blazor.Shared;

using Microsoft.AspNetCore.Components;

namespace Demo.Net.Blazor.App.Pages
{
	public partial class Index : ComponentBase, IWorkspace
	{
		public Type Type { get => GetType(); }
		public string Name { get => nameof(Index); }
		public Dictionary<string, object?> Parameters { get; }
	}
}
