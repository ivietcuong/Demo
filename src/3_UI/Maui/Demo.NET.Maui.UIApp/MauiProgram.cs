using Demo.Net.Infrast.Impl.SQLiteService;
using Demo.Net.Maui.Shared.ViewModels;
using Demo.Net.Maui.SQLitePresenter.ViewModels;
using Demo.Net.Maui.SQLitePresenter.Views;
using Demo.Net.Maui.UIApp.Services;
using Demo.Net.Maui.UIApp.ViewModels;
using Demo.Net.Maui.UIApp.Views;
using Demo.NetStandard.Core.Interfaces;
using Demo.NetStandard.Core.Services;

using Microsoft.EntityFrameworkCore;
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

			RegisterDatabase(builder);
			RegisterMathServices(builder);
			RegisterViewModels(builder);
			RegisterWorkspaces(builder);
			RegisterViews(builder);

			return builder.Build();
		}
		
		private static void RegisterViews(MauiAppBuilder builder)
		{			
			builder.Services.AddSingleton<AppShell>();
			builder.Services.AddSingleton<MainPage>();
		}

		private static void RegisterDatabase(MauiAppBuilder builder)
		{
			builder.Services.AddSingleton<IFileSystem>(FileSystem.Current);
			builder.Services.AddSingleton<IFileService, FileService>();

			builder.Services.AddScoped<IUnitOfWork, SQLiteContext>(provider =>
			{
				var optionsBuilder = new DbContextOptionsBuilder<SQLiteContext>();
				optionsBuilder.UseSqlite($"Data Source={Path.Combine(FileSystem.Current.AppDataDirectory, "points.db")}");
				return (SQLiteContext)ActivatorUtilities.CreateInstance(provider, typeof(SQLiteContext), optionsBuilder.Options);
			});

			builder.Services.AddScoped<IAsyncRepository, AsyncSQLiteRepository>();
			builder.Services.AddScoped<IPointService, SQLitePointService>();
		}

		private static void RegisterWorkspaces(MauiAppBuilder builder)
		{
			builder.Services.AddScoped<IWorkspace, HomeView>();
			builder.Services.AddScoped<IWorkspace, MathServiceView>();
		}

		private static void RegisterViewModels(MauiAppBuilder builder)
		{
			builder.Services.AddScoped<MainPageViewModel>();
			builder.Services.AddScoped<HomeViewModel>();
			builder.Services.AddScoped<MathServiceViewModel>();
		}

		private static void RegisterMathServices(MauiAppBuilder builder)
		{			
			builder.Services.AddScoped<IMathService, TangentMathService>();
			builder.Services.AddScoped<IMathService, ParabolaMathService>();
			builder.Services.AddScoped<IMathService, LogarithmMathService>();
			builder.Services.AddScoped<IMathService, ExponentiationMathService>();
		}

	}
}