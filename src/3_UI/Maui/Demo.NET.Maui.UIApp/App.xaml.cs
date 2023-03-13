namespace Demo.NET.Maui.UIApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        public App(MainPage mainPage)
            : this()
        {
            MainPage = mainPage;
        }
    }
}