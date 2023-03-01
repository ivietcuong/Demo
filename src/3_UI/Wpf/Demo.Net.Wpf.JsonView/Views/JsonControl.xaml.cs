using Demo.Net.Wpf.Core;
using Demo.Net.Wpf.JsonView.ViewModels;

using System.Windows.Controls;

namespace Demo.Net.Wpf.JsonView.Views
{
	/// <summary>
	/// Interaction logic for JsonControl.xaml
	/// </summary>
	public partial class JsonControl : UserControl, IView
	{
		public JsonControl()
		{
			InitializeComponent();
		}

		public JsonControl(JsonControlViewModel viewModel)
			: this()
		{
			DataContext = viewModel;
		}
	}
}
