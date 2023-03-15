using Demo.Net.Maui.Shared.ViewModels;
using Demo.Net.Maui.UIApp.ViewModels;

namespace Demo.Net.Maui.UIApp.Views;

public partial class HomeView : ContentView, IWorkspace
{
	public HomeView()
	{
		InitializeComponent();		
	}

	public HomeView(HomeViewModel viewModel)
		: this()
	{
		BindingContext = viewModel;
	}

	public string Icon { get; set; }
	public string Description { get; set; }
}