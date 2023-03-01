using Demo.Net.Wpf.Core;
using Demo.Net.Wpf.JsonView.ViewModels;
using Demo.Net.Wpf.JsonView.Views;
using Demo.Net.Wpf.XmlView.ViewModels;
using Demo.Net.Wpf.XmlView.Views;
using Demo.Net.WpfApp.ViewModels;
using Demo.Net.WpfApp.Views;
using Demo.NetStandard.Core.Interfaces;
using Demo.NetStandard.Core.Services;
using Demo.NetStandard.Infrast.Impl.JsonService;
using Demo.NetStandard.Infrast.Impl.XmlService;
using Demo.NetStandard.Infrast.JsonService.Impl;
using Demo.NetStandard.Infrast.XmlService.Impl;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Windows;

namespace Demo.Net.WpfApp
{
    public partial class App : Application
	{
		public IServiceProvider? ServiceProvider { get; private set; }

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			Initialize();
		}
		

		private void Initialize()
		{
			var services = new ServiceCollection();

			RegisterShell(services);
			RegisterServices(services);
			RegisterViews(services);

			ServiceProvider = services.BuildServiceProvider();
			var shell = ServiceProvider.GetService<Shell>();
			shell?.Show();
		}

		private void RegisterViews(ServiceCollection serviceCollection)
		{
			serviceCollection.AddTransient<XmlControlViewModel>(provider =>
			{
				var service = ActivatorUtilities.GetServiceOrCreateInstance(provider, typeof(XmlPointService));
				var viewmodel = ActivatorUtilities.CreateInstance(provider, typeof(XmlControlViewModel), service);
				return (XmlControlViewModel)viewmodel;
			});
			serviceCollection.AddTransient<IWorkspace, XmlControl>();

			serviceCollection.AddTransient<JsonControlViewModel>(provider =>
			{
				var service = ActivatorUtilities.GetServiceOrCreateInstance(provider, typeof(JsonPointService));
				var viewmodel = ActivatorUtilities.CreateInstance(provider, typeof(JsonControlViewModel), service);
				return (JsonControlViewModel)viewmodel;
			});
			serviceCollection.AddTransient<IWorkspace, JsonControl>();
		}

		private ServiceCollection RegisterServices(ServiceCollection serviceCollection)
		{
			serviceCollection.AddTransient<IUnitOfWork, JsonContext>();
			serviceCollection.AddTransient<IAsyncRepository, AsyncJsonRepository>();
			serviceCollection.AddTransient<IPointService, JsonPointService>(provider =>
			{
				var jsoncontext = ActivatorUtilities.CreateInstance(provider, typeof(JsonContext));
				var jsonrepository = ActivatorUtilities.CreateInstance(provider, typeof(AsyncJsonRepository), jsoncontext);
				var jsonpointservice = ActivatorUtilities.CreateInstance(provider, typeof(JsonPointService), jsonrepository);
				return (JsonPointService)jsonpointservice;
			});

			serviceCollection.AddTransient<IUnitOfWork, XmlContext>();
			serviceCollection.AddTransient<IAsyncRepository, AsyncXmlRepository>();
			serviceCollection.AddTransient<IPointService, XmlPointService>(provider =>
			{
				var xmlcontext = ActivatorUtilities.CreateInstance(provider, typeof(XmlContext));
				var xmlrepository = ActivatorUtilities.CreateInstance(provider, typeof(AsyncXmlRepository), xmlcontext);
				var xmlpointservice = ActivatorUtilities.CreateInstance(provider, typeof(XmlPointService), xmlrepository);
				return (XmlPointService)xmlrepository;
			});

			return serviceCollection;
		}

		private static ServiceCollection RegisterShell(ServiceCollection servicecollection)
		{
			servicecollection.AddSingleton<ShellViewModel>();
			servicecollection.AddSingleton<Shell>();
			return servicecollection;
		}
	}
}
