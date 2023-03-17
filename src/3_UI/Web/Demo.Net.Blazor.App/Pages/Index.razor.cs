using Demo.Net.Blazor.Shared;

namespace Demo.Net.Blazor.App.Pages
{
    public partial class Index : WorkspaceBase, IWorkspace
    {        
        public Type Type
        {
            get => GetType();
        }

        public string Name
        {
            get => nameof(Index);
        }              
    }
}
