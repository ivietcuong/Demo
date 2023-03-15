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

			RegisterMathServices(builder);
			RegisterViewModels(builder);
			RegisterWorkspaces(builder);
			RegisterViews(builder);

			return builder.Build();
		}
		
		private static void RegisterViews(MauiAppBuilder builder)
		{
			builder.Services.AddSingleton<IFileSystem>(FileSystem.Current);
			builder.Services.AddSingleton<AppShell>();
			builder.Services.AddSingleton<MainPage>();
		}

		private static void RegisterWorkspaces(MauiAppBuilder builder)
		{
			builder.Services.AddSingleton<IWorkspace, HomeView>();
			builder.Services.AddSingleton<IWorkspace, MathServiceView>();
		}

		private static void RegisterViewModels(MauiAppBuilder builder)
		{
			builder.Services.AddSingleton<MainPageViewModel>();
			builder.Services.AddSingleton<HomeViewModel>();
			builder.Services.AddSingleton<MathServiceViewModel>();
		}

		//https://learn.microsoft.com/en-us/training/modules/store-local-data/
		//builder.Services.AddSingleton<IFileSystem>(FileSystem.Current);
		private static void RegisterMathServices(MauiAppBuilder builder)
		{
			string databasepath = $"Data Source={Path.Combine(FileSystem.Current.AppDataDirectory, "points.db")}";
			var optionsBuilder = new DbContextOptionsBuilder<SQLiteContext>();
			optionsBuilder.UseSqlite(databasepath);

			builder.Services.AddSingleton<DbContextOptionsBuilder<SQLiteContext>>(optionsBuilder);
			builder.Services.AddSingleton<DbContextOptions<SQLiteContext>>(optionsBuilder.Options);

			//builder.Services.AddSingleton<IUnitOfWork, SQLiteContext>(provider => 
			//{
			//	var options =(DbContextOptions<SQLiteContext>) ActivatorUtilities.CreateInstance(provider, typeof(DbContextOptions<SQLiteContext>));
			//	return (SQLiteContext)ActivatorUtilities.CreateInstance(provider, typeof(SQLiteContext), options);
			//});

			builder.Services.AddSingleton<IUnitOfWork, SQLiteContext>(provider =>
			{
				var optionsBuilder = new DbContextOptionsBuilder<SQLiteContext>();
				optionsBuilder.UseSqlite(databasepath);
				return (SQLiteContext)ActivatorUtilities.CreateInstance(provider, typeof(SQLiteContext), optionsBuilder.Options);
			});

			builder.Services.AddSingleton<IAsyncRepository, AsyncSQLiteRepository>();
			builder.Services.AddSingleton<IPointService, SQLitePointService>();


			builder.Services.AddSingleton<IMathService, TangentMathService>();
			builder.Services.AddSingleton<IMathService, ParabolaMathService>();
			builder.Services.AddSingleton<IMathService, LogarithmMathService>();
			builder.Services.AddSingleton<IMathService, ExponentiationMathService>();
		}
	}
}