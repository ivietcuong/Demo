using Demo.Net.Blazor.App.Services;
using Demo.Net.Blazor.Shared;

using Microsoft.AspNetCore.Components;

using System.Diagnostics.CodeAnalysis;

namespace Demo.Net.Blazor.App.Pages
{
	public partial class ParabolaMath : ComponentBase, IWorkspace
	{
		public Type Type 
		{
			get => GetType();
		}

		public string Name 
		{
			get => "Parabola";
		}

		public Dictionary<string, object?> Parameters { get; } = new Dictionary<string, object?>();

		[Inject, AllowNull]
		public IParabolaMathService? MathService { get; set; }
	}
}
