using Demo.Net.Blazor.App.Pages;
using Demo.Net.Blazor.App.Services;
using Demo.Net.Blazor.Shared;

using Microsoft.AspNetCore.Components;

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

			builder.Services.AddSingleton<IComponent, Pages.Index>();
			builder.Services.AddSingleton<IWorkspace, TangentMath>();
			builder.Services.AddSingleton<IWorkspace, ParabolaMath>();
			builder.Services.AddSingleton<IWorkspace, LogarithmMath>();
			builder.Services.AddSingleton<IWorkspace, ExponentiationMath>();

			builder.Services.AddSingleton<ITangentMathService, TangentMathService>();
			builder.Services.AddSingleton<IParabolaMathService, ParabolaMathService>();
			builder.Services.AddSingleton<ILogarithmMathService, LogarithmMathService>();
			builder.Services.AddSingleton<IExponentiationMathService, ExponentiationMathService>();

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