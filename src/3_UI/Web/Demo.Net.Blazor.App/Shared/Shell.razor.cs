using Demo.Net.Blazor.Shared;

using Microsoft.AspNetCore.Components;

namespace Demo.Net.Blazor.App.Shared
{
	public partial class Shell : ComponentBase
	{
		private int _index = 0;

        private bool _collapseNavMenu = true;
        private string? NavMenuCssClass => _collapseNavMenu ? "collapse" : null;

        public IWorkspace? Workspace { get; set; }

		[Inject]
		IEnumerable<IWorkspace>? Workspaces { get; set; }

		public Shell()
		{

		}

		protected override void OnInitialized()
		{
			base.OnInitialized();
		}

		private void NavigateToWorkspace(IWorkspace workspace)
		{
			if (_index == 3)
				_index = 0;

			Workspace = Workspaces?.ElementAt(_index);
			_index++;
		}


        private void ToggleNavMenu()
        {
            _collapseNavMenu = !_collapseNavMenu;
        }
    }
}
