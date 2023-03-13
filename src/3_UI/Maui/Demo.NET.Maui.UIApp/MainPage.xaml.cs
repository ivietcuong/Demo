namespace Demo.NET.Maui.UIApp
{
    public partial class MainPage : FlyoutPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public MainPage(MainPageViewModel viewModel)
            : this()
        {
            BindingContext = viewModel;
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IsPresented = false;
        }
    }
}