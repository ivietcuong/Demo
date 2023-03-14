using Demo.Net.Maui.Shared.ViewModels;
using Demo.Net.Maui.SQLitePresenter.ViewModels;

namespace Demo.Net.Maui.SQLitePresenter.Views;

public partial class MathServiceView : ContentView, IWorkspace
{
    public string? Icon { get; set; }
    public string? Description { get; set; }

    public MathServiceView()
    {
        InitializeComponent();
    }

    public MathServiceView(MathServiceViewModel viewModel)
        : this()
    {
        BindingContext = viewModel;
    }
}