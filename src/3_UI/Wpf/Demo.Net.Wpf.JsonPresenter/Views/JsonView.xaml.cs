using Demo.Net.Wpf.JsonPresenter.ViewModels;

using System.Windows.Controls;
using Demo.Net.Wpf.Shared.ViewModels;
using Demo.Net.Wpf.Shared;

namespace Demo.Net.Wpf.JsonPresenter.Views
{
    public partial class JsonView : UserControl, IWorkspace
	{
		public string? Icon { get; set; } = Icons.Json;
		public string? Description { get; set; } = ViewDesriptions.JsonView;

		public JsonView()
		{
			InitializeComponent();
		}
		public JsonView(JsonViewModel viewModel)
			: this()
		{
			DataContext = viewModel;
		}

	}
}
