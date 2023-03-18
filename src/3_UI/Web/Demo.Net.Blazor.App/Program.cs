using Demo.Net.Blazor.App.Pages;
using Demo.Net.Blazor.App.Services;
using Demo.Net.Blazor.Shared;
using Demo.Net.Infrast.Impl.SQLiteService;
using Demo.NetStandard.Core.Interfaces;
using Demo.NetStandard.Core.Services;

using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

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

            RegisterDatabase(builder);

            RegisterPages(builder);

            RegisterServices(builder);

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
            builder.Services.AddDbContextFactory<SQLiteContext>(optionsbuilder =>
            {
                optionsbuilder.UseSqlite($"Data Source=data/points.db");
                var builder = new DbContextOptionsBuilder<SQLiteContext>((DbContextOptions<SQLiteContext>)optionsbuilder.Options)
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