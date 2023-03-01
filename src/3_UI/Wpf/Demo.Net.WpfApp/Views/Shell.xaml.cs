using Demo.Net.WpfApp.ViewModels;

using System.Windows;

namespace Demo.Net.WpfApp.Views
{
	public partial class Shell : Window
	{
		public Shell()
		{
			InitializeComponent();
		}
		public Shell(ShellViewModel viewModel)
			: this()
		{
			DataContext = viewModel;
		}
	}
}
