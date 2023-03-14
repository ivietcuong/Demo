using Demo.Net.Maui.UIApp.Services;
using Demo.Net.Maui.UIApp.ViewModels;
using Demo.Net.Maui.UIApp.Views;
using Demo.NetStandard.Core.Services;

using Microsoft.Extensions.Logging;

using OxyPlot.Maui.Skia;

using SkiaSharp.Views.Maui.Controls.Hosting;

namespace Demo.Net.Maui.UIApp
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder.UseMauiApp<App>()
					.UseSkiaSharp()
					.UseOxyPlotSkia()
					.ConfigureFonts(fonts =>
					{
						fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
						fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
					});

#if DEBUG
			builder.Logging.AddDebug();
#endif

			RegisterMathServices(builder);

			builder.Services.AddSingleton<HomeViewModel>();
			builder.Services.AddSingleton<HomeView>();
			builder.Services.AddSingleton<AppShell>();
			builder.Services.AddSingleton<MainPageViewModel>();
			builder.Services.AddSingleton<MainPage>();

			return builder.Build();
		}

		private static void RegisterMathServices(MauiAppBuilder builder)
		{
			builder.Services.AddSingleton<IMathService, TangentMathService>();
			builder.Services.AddSingleton<IMathService, ParabolaMathService>();
			builder.Services.AddSingleton<IMathService, LogarithmMathService>();
			builder.Services.AddSingleton<IMathService, ExponentiationMathService>();
		}
	}
}