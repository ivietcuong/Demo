using System;
using System.Windows;

namespace Demo.Net.WpfApp
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public IServiceProvider Services { get; }
		public MainWindow()
		{
			Services = ConfigureServices();
			InitializeComponent();
		}

		private IServiceProvider ConfigureServices()
		{
			throw new NotImplementedException();
		}
	}
}
