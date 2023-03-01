using Demo.Net.Wpf.Core;
using Demo.Net.Wpf.XmlView.ViewModels;

using System.Windows.Controls;

namespace Demo.Net.Wpf.XmlView.Views
{
	/// <summary>
	/// Interaction logic for XmlControl.xaml
	/// </summary>
	public partial class XmlControl : UserControl, IWorkspace
	{
		public XmlControl()
		{
			InitializeComponent();
		}

		public XmlControl(XmlControlViewModel viewModel)
			: this()
		{
			DataContext = viewModel;
		}
	}
}
