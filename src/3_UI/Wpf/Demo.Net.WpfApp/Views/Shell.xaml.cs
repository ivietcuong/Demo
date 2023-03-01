using Demo.Net.WpfApp.ViewModels;

using System.Windows;

namespace Demo.Net.WpfApp.Views
{
	/// <summary>
	/// Interaction logic for Shell.xaml
	/// </summary>
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
