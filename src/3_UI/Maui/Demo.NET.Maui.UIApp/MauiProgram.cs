using Demo.NET.Maui.UIApp.ViewModels;
using Demo.NET.Maui.UIApp.Views;

using Microsoft.Extensions.Logging;

namespace Demo.NET.Maui.UIApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<HomeViewModel>();
            builder.Services.AddSingleton<HomeView>();
            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddSingleton<MainPage>();

            return builder.Build();
        }
    }
}