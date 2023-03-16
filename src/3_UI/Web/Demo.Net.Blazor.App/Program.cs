using Demo.Net.Blazor.App.Data;
using Demo.Net.Blazor.App.Pages;
using Demo.Net.Blazor.Shared;

namespace Demo.Net.Blazor.App
{
	//https://learn.microsoft.com/de-de/aspnet/core/blazor/tutorials/signalr-blazor?view=aspnetcore-7.0&tabs=visual-studio&pivots=server
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddRazorPages();
			builder.Services.AddServerSideBlazor();

			builder.Services.AddSingleton<WeatherForecastService>();
			builder.Services.AddSingleton<IWorkspace, Pages.Index>();
			builder.Services.AddSingleton<IWorkspace, Counter>();
			builder.Services.AddSingleton<IWorkspace, FetchData>();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();

			app.UseStaticFiles();

			app.UseRouting();

			app.MapBlazorHub();
			app.MapFallbackToPage("/_Host");

			app.Run();
		}
	}
}