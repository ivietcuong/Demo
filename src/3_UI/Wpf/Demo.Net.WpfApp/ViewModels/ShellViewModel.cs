using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Demo.Net.Wpf.Shared;

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Demo.Net.WpfApp.ViewModels
{

	public partial class ShellViewModel : ObservableObject
	{

		[ObservableProperty]
		private object? _workspace;

		[ObservableProperty]
		private ObservableCollection<IWorkspace> _workspaces;

		public ShellViewModel(IEnumerable<IWorkspace> views)
		{
			_workspaces = new ObservableCollection<IWorkspace>(views);
		}

		[RelayCommand]
		private void SelectWorkspace(IWorkspace workspace)
		{
			Workspace = workspace;
		}
	}
}
