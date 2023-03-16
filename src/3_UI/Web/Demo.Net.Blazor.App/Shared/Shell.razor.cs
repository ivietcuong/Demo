using Demo.Net.Blazor.App.Data;
using Demo.Net.Blazor.App.Pages;
using Demo.Net.Blazor.Shared;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Demo.Net.Blazor.App.Shared
{
    public partial class Shell : LayoutComponentBase
    {
        private int count = 0;
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
            if (count % 2 == 0)
                Workspace = Workspaces.FirstOrDefault();
            else
                Workspace = Workspaces.LastOrDefault();

            count++;

            Body = BuildComponent;
        }

        private void BuildComponent(RenderTreeBuilder builder)
        {
            if (Workspace == null)
                return;

            int sequence = 1;

            builder.OpenComponent(0, Workspace.Type);

            if (Workspace.Properties != null)
                foreach (var p in Workspace.Properties)
                    builder.AddAttribute(sequence++, p.Key, p.Value);

            builder.CloseComponent();
        }
    }
}
