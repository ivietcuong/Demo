using Demo.Net.Blazor.App.Pages;
using Demo.Net.Blazor.App.Services;
using Demo.Net.Blazor.Shared;
using Demo.Net.Infrast.Impl.SQLiteService;

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
            builder.Services.AddSingleton<IComponent, Pages.Index>();
            builder.Services.AddSingleton<IWorkspace, TangentMath>();
            builder.Services.AddSingleton<IWorkspace, ParabolaMath>();
            builder.Services.AddSingleton<IWorkspace, LogarithmMath>();
            builder.Services.AddSingleton<IWorkspace, ExponentiationMath>();
        }

        /// <summary>
        /// <remarks>
        ///     <para>
        ///         Use this method when using dependency injection in your application, such as with ASP.NET Core.
        ///         For applications that don't use dependency injection, consider creating <see cref="DbContext" />
        ///         instances directly with its constructor. The <see cref="DbContext.OnConfiguring" /> method can then be
        ///         overridden to configure a connection string and other options.
        ///     </para>
        ///     <para>
        ///         Entity Framework Core does not support multiple parallel operations being run on the same <see cref="DbContext" />
        ///         instance. This includes both parallel execution of async queries and any explicit concurrent use from multiple threads.
        ///         Therefore, always await async calls immediately, or use separate DbContext instances for operations that execute
        ///         in parallel. See <see href="https://aka.ms/efcore-docs-threading">Avoiding DbContext threading issues</see> for more information
        ///         and examples.
        ///     </para>
        ///     <para>
        ///         See <see href="https://aka.ms/efcore-docs-di">Using DbContext with dependency injection</see> for more information and examples.
        ///     </para>
        /// </remarks>
        /// <param name="builder"></param>
        private static void RegisterDatabase(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContextFactory<SQLiteContext>(opt => opt.UseSqlite($"Data Source=data/points.db"));
        }

        private static void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<ITangentMathService, TangentMathService>();
            builder.Services.AddSingleton<IParabolaMathService, ParabolaMathService>();
            builder.Services.AddSingleton<ILogarithmMathService, LogarithmMathService>();
            builder.Services.AddSingleton<IExponentiationMathService, ExponentiationMathService>();
        }

    }
}