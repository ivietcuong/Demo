using Demo.Net.Blazor.Shared;

using Microsoft.AspNetCore.Components;

using System.Diagnostics.CodeAnalysis;

namespace Demo.Net.Blazor.App.Shared
{
	public partial class Shell : ComponentBase
	{
		private bool _collapseNavMenu = true;
		private string? NavMenuCssClass => _collapseNavMenu ? "collapse" : null;

		public IWorkspace? Workspace 
		{
			get; set; 
		}

		[Inject, AllowNull]
		public IEnumerable<IWorkspace>? Workspaces 
		{
			get; set; 
		}

		[Inject, AllowNull]
		public IComponent? Home 
		{
			get; set; 
		}
		private void OnHome()
		{
			Workspace = (IWorkspace?)Home; 
		}

		private void ToggleNavMenu()
		{
			_collapseNavMenu = !_collapseNavMenu;
		}

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
			OnHome();
        }

        private void NavigateToWorkspace(IWorkspace workspace)
		{
			Workspace = workspace;
		}
	}
}
