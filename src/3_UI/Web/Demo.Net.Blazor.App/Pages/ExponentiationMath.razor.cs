using Demo.Net.Blazor.App.Services;
using Demo.Net.Blazor.Shared;

using Microsoft.AspNetCore.Components;

using System.Diagnostics.CodeAnalysis;

namespace Demo.Net.Blazor.App.Pages
{
	public partial class ExponentiationMath : ComponentBase, IWorkspace
	{
		public Type Type 
		{
			get => GetType(); 
		}

		public string Name 
		{
			get => "Exponentiation"; 
		}

		public Dictionary<string, object?> Parameters { get; } = new Dictionary<string, object?>();

		[Inject, AllowNull]
		public IExponentiationMathService? MathService { get; set; }

		protected override async Task OnParametersSetAsync()
		{
			await base.OnParametersSetAsync();

			var query = MathService?.Description;
		}
	}
}
