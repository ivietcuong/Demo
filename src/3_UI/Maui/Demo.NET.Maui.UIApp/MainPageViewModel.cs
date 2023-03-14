
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

using Demo.Net.Maui.Shared.Services;
using Demo.Net.Maui.Shared.ViewModels;
using Demo.NetStandard.Core.Services;

using System.Collections.ObjectModel;

namespace Demo.Net.Maui.UIApp
{
    public partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<IMathService> _mathServices;

        [ObservableProperty]
        private IMathService _selectedMathService;

        [ObservableProperty]
        private object _workspace;

        [ObservableProperty]
        private List<IWorkspace> _workspaces;

        public MainPageViewModel(IEnumerable<IWorkspace> workspaces, IEnumerable<IMathService> mathservices)
        {
            Workspaces = new List<IWorkspace>(workspaces);
            MathServices = new ObservableCollection<IMathService>(mathservices);

            Workspace = Workspaces.FirstOrDefault();
        }

        partial void OnSelectedMathServiceChanged(IMathService value)
        {
            Workspace = Workspaces.LastOrDefault();
            WeakReferenceMessenger.Default.Send(new MathServiceChangedMessage(SelectedMathService));
        }

    }
}
