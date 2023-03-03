using Demo.Net.Wpf.Shared;
using Demo.Net.Wpf.XmlPresenter.ViewModels;

using System.Windows.Controls;

namespace Demo.Net.Wpf.XmlPresenter.Views
{
	public partial class XmlView : UserControl, IWorkspace
	{
		public string? Icon { get; set; } = Icons.Xml;

		public string? Description { get; set; } = ViewDesriptions.XmlView;

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
