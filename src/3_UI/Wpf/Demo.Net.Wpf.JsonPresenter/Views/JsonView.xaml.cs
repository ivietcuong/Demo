﻿using Demo.Net.Wpf.Core;
using Demo.Net.Wpf.JsonPresenter.ViewModels;

using System.Windows.Controls;

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
		public JsonView(JsonControlViewModel viewModel)
			: this()
		{
			DataContext = viewModel;
		}

	}
}
