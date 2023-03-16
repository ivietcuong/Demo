using Demo.Net.Blazor.Shared;

using Microsoft.AspNetCore.Components;

namespace Demo.Net.Blazor.App.Shared
{
	public partial class Shell : ComponentBase
	{
		private int index = 0;
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

		private void NavigateToWorkspace()
		{
			if (index == 3)
				index = 0;

			Workspace = Workspaces?.ElementAt(index);
			index++;
		}
	}
}
