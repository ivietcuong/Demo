using Demo.Net.Blazor.App.Pages;
using Demo.Net.Blazor.App.Services;
using Demo.Net.Blazor.Shared;
using Demo.Net.Infrast.Impl.SQLiteService;
using Demo.NetStandard.Core.Interfaces;
using Demo.NetStandard.Core.Services;

using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

using NLog;
using NLog.Web;

using System;

namespace Demo.Net.Blazor.App
{
	//https://learn.microsoft.com/de-de/aspnet/core/blazor/tutorials/signalr-blazor?view=aspnetcore-7.0&tabs=visual-studio&pivots=server
	public class Program
	{
		public static void Main(string[] args)
		{
			var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
			logger.Debug("init main");

			try
			{
				var builder = WebApplication.CreateBuilder(args);

				builder.Services.AddRazorPages();
				builder.Services.AddServerSideBlazor();

				builder.Logging.ClearProviders();
				builder.Host.UseNLog();

				RegisterDatabase(builder);

				RegisterPages(builder);

				RegisterServices(builder);

				var app = builder.Build();

				if (!app.Environment.IsDevelopment())
				{
					app.UseExceptionHandler("/Error");
					app.UseHsts();
				}

				app.UseHttpsRedirection();

				app.UseStaticFiles();

				app.UseRouting();

				app.MapBlazorHub();
				app.MapFallbackToPage("/_Host");

				app.Run();

			}
			catch (Exception exception)
			{
				logger.Error(exception, "Stopped program because of exception");
				throw;
			}
			finally
			{
				LogManager.Shutdown();
			}
		}

		private static void RegisterPages(WebApplicationBuilder builder)
		{
			builder.Services.AddScoped<IComponent, Pages.Index>();
			builder.Services.AddScoped<IWorkspace, TangentMath>();
			builder.Services.AddScoped<IWorkspace, ParabolaMath>();
			builder.Services.AddScoped<IWorkspace, LogarithmMath>();
			builder.Services.AddScoped<IWorkspace, ExponentiationMath>();
		}

		private static void RegisterDatabase(WebApplicationBuilder builder)
		{
			builder.Services.AddDbContextFactory<SQLiteContext>(dbContextOptionsBuilder =>
            {
                dbContextOptionsBuilder.UseSqlite($"Data Source=data/points.db");
                new DbContextOptionsBuilder<SQLiteContext>((DbContextOptions<SQLiteContext>)dbContextOptionsBuilder.Options)
                    .UseLoggerFactory(new LoggerFactory());
            });
			builder.Services.AddScoped<IUnitOfWork, SQLiteContext>(provider =>
			{
				var options = provider.GetRequiredService<DbContextOptions<SQLiteContext>>();
				return (SQLiteContext)ActivatorUtilities.CreateInstance(provider, typeof(SQLiteContext), options);
			});

			builder.Services.AddScoped<IAsyncRepository, AsyncSQLiteRepository>();
			builder.Services.AddScoped<IPointService, SQLitePointService>();
		}

		private static void RegisterServices(WebApplicationBuilder builder)
		{
			builder.Services.AddScoped<ITangentMathService, TangentMathService>();
			builder.Services.AddScoped<IParabolaMathService, ParabolaMathService>();
			builder.Services.AddScoped<ILogarithmMathService, LogarithmMathService>();
			builder.Services.AddScoped<IExponentiationMathService, ExponentiationMathService>();
		}

	}
}