using CommunityToolkit.Mvvm.ComponentModel;

using Demo.Net.Wpf.Core;

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Demo.Net.WpfApp.ViewModels
{

	public partial class ShellViewModel : ObservableObject
    {

        [ObservableProperty]
        private object? _content;

        [ObservableProperty]
        private ObservableCollection<IWorkspace> _workspaces;

        public ShellViewModel(IEnumerable<IWorkspace> views)
        {
            _workspaces = new ObservableCollection<IWorkspace>(views);
        }
    }
}
