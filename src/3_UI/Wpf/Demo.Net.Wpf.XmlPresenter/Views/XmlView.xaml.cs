using Demo.Net.Wpf.Core;
using Demo.Net.Wpf.XmlPresenter.ViewModels;

using System.Windows.Controls;

namespace Demo.Net.Wpf.XmlPresenter.Views
{
	public partial class XmlView : UserControl, IWorkspace
	{
		public XmlView()
		{
			InitializeComponent();
		}

		public XmlView(XmlControlViewModel viewModel)
			: this()
		{
			DataContext = viewModel;
		}
	}
}
