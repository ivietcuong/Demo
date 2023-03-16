namespace Demo.Net.Maui.UIApp.Views;

public partial class AppShell : ContentPage
{
	public AppShell()
	{
		InitializeComponent();
#if WINDOWS
		NavigationPage.SetHasNavigationBar(this,false);
#endif
	}
}