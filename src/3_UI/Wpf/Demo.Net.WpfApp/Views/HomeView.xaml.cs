using Demo.Net.Wpf.Shared;
using Demo.Net.WpfApp.ViewModels;

using System.Windows.Controls;

namespace Demo.Net.WpfApp.Views
{
	public partial class HomeView : UserControl, IWorkspace
	{
		public string? Icon { get; set; } = Icons.Home;
		public string? Description { get; set; } = ViewDesriptions.HomeView;

		public HomeView()
		{
			InitializeComponent();
		}

		public HomeView(HomeViewModel viewModel)
		{
			DataContext = viewModel;
		}

	}
}
