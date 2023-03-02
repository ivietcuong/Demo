using Demo.Net.Wpf.Core;
using Demo.Net.Wpf.JsonPresenter.ViewModels;
using Demo.Net.Wpf.JsonPresenter.Views;
using Demo.Net.Wpf.XmlPresenter.ViewModels;
using Demo.Net.Wpf.XmlPresenter.Views;
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
using System.Net.Http.Json;
using System.Windows;

namespace Demo.Net.WpfApp
{
	public partial class App : Application
	{
		public IServiceProvider? ServiceProvider { get; private set; }

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			Register();
			//Initialize();
		}

		private void Register()
		{
			var serviceCollection = new ServiceCollection();

			serviceCollection.AddTransient<IUnitOfWork, JsonContext>();
			serviceCollection.AddTransient<IAsyncRepository, AsyncJsonRepository>();
			serviceCollection.AddTransient<IPointService, JsonPointService>();

			serviceCollection.AddTransient<IUnitOfWork, XmlContext>();
			serviceCollection.AddTransient<IAsyncRepository, AsyncXmlRepository>();
			serviceCollection.AddTransient<IPointService, XmlPointService>();

			//serviceCollection.AddTransient(provider =>
			//{
			//	var xmlcontext = ActivatorUtilities.CreateInstance(provider, typeof(XmlContext));
			//	var xmlrepository = ActivatorUtilities.CreateInstance(provider, typeof(AsyncXmlRepository), xmlcontext);
			//	var service = ActivatorUtilities.CreateInstance(provider, typeof(XmlPointService), xmlrepository);
			//	var viewmodel = ActivatorUtilities.CreateInstance(provider, typeof(XmlControlViewModel), service);
			//	return (XmlControlViewModel)viewmodel;
			//});

			//serviceCollection.AddTransient(provider =>
			//{
			//	var jsoncontext = ActivatorUtilities.CreateInstance(provider, typeof(JsonContext));
			//	var jsonrepository = ActivatorUtilities.CreateInstance(provider, typeof(AsyncJsonRepository), jsoncontext);
			//	var service = ActivatorUtilities.CreateInstance(provider, typeof(JsonPointService), jsonrepository);
			//	var viewmodel = ActivatorUtilities.CreateInstance(provider, typeof(JsonControlViewModel), service);
			//	return (JsonControlViewModel)viewmodel;
			//});

			RegisterViews(serviceCollection);
			RegisterShell(serviceCollection);

			ServiceProvider = serviceCollection.BuildServiceProvider();

			var pointServices = ServiceProvider.GetServices<IPointService>();

			var xmlservice = ActivatorUtilities.CreateInstance(ServiceProvider, typeof(XmlPointService));
			var jsonservice = ActivatorUtilities.CreateInstance(ServiceProvider, typeof(JsonPointService));

			var xmlcontrolviewmodel = ServiceProvider.GetRequiredService<XmlControlViewModel>();
			var jsoncontrolviewmodel = ServiceProvider.GetService<JsonControlViewModel>();

			var xmlcontrolviewmodel_ = ActivatorUtilities.CreateInstance(ServiceProvider, typeof(XmlControlViewModel));
			var jsoncontrolviewmodel_ = ActivatorUtilities.CreateInstance(ServiceProvider, typeof(JsonControlViewModel));

			var shell = ServiceProvider.GetService<Shell>();
			shell?.Show();
		}

		private void Initialize()
		{
			var serviceCollection = new ServiceCollection();

			RegisterServices(serviceCollection);
			RegisterViews(serviceCollection);
			RegisterShell(serviceCollection);


			ServiceProvider = serviceCollection.BuildServiceProvider();

			var pointServices = ServiceProvider.GetServices<IPointService>();


			var xmlservice = ActivatorUtilities.CreateInstance(ServiceProvider, typeof(XmlPointService));
			var jsonservice = ActivatorUtilities.CreateInstance(ServiceProvider, typeof(JsonPointService));


			var shell = ServiceProvider.GetService<Shell>();
			shell?.Show();
		}

		private void RegisterViews(ServiceCollection serviceCollection)
		{
			serviceCollection.AddTransient<XmlControlViewModel>(provider =>
			{
				var xmlcontext = ActivatorUtilities.CreateInstance(provider, typeof(XmlContext));
				var xmlrepository = ActivatorUtilities.CreateInstance(provider, typeof(AsyncXmlRepository), xmlcontext);
				var service = ActivatorUtilities.CreateInstance(provider, typeof(XmlPointService), xmlrepository);
				var viewmodel = ActivatorUtilities.CreateInstance(provider, typeof(XmlControlViewModel), service);
				return (XmlControlViewModel)viewmodel;
			});
			serviceCollection.AddTransient<IWorkspace, XmlView>();

			serviceCollection.AddTransient<JsonControlViewModel>(provider =>
			{
				var jsoncontext = ActivatorUtilities.CreateInstance(provider, typeof(JsonContext));
				var jsonrepository = ActivatorUtilities.CreateInstance(provider, typeof(AsyncJsonRepository), jsoncontext);
				var service = ActivatorUtilities.CreateInstance(provider, typeof(JsonPointService), jsonrepository);
				var viewmodel = ActivatorUtilities.CreateInstance(provider, typeof(JsonControlViewModel), service);
				return (JsonControlViewModel)viewmodel;
			});
			serviceCollection.AddTransient<IWorkspace, JsonView>();
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
				return (XmlPointService)xmlpointservice;
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
