
using CommunityToolkit.Mvvm.ComponentModel;

using Demo.Net.Maui.UIApp.Views;
using Demo.NetStandard.Core.Services;

using System.Collections.ObjectModel;

namespace Demo.Net.Maui.UIApp
{
	public partial class MainPageViewModel : ObservableObject
	{
		[ObservableProperty]
		private ObservableCollection<IMathService> _mathServices = new ObservableCollection<IMathService>();

		[ObservableProperty]
		private IMathService _selectedMathService;

		[ObservableProperty]
		private object _workspace;

		public MainPageViewModel(IEnumerable<IMathService> mathservices)
		{
			MathServices = new ObservableCollection<IMathService>(mathservices);
			Workspace = new HomeView();
		}
	}
}
