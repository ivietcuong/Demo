﻿using Demo.Net.Wpf.Core;
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
			var serviceCollection = new ServiceCollection();

			RegisterServices(serviceCollection);
			RegisterViewModels(serviceCollection);
			RegisterViews(serviceCollection);
			RegisterShell(serviceCollection);

			ServiceProvider = serviceCollection.BuildServiceProvider();
			MainWindow = ServiceProvider.GetService<Shell>();
			MainWindow?.Show();
		}

		private void RegisterViews(ServiceCollection serviceCollection)
		{			
			serviceCollection.AddTransient<IWorkspace, HomeView>();
			serviceCollection.AddTransient<IWorkspace, XmlView>();
			serviceCollection.AddTransient<IWorkspace, JsonView>();
		}

		private static void RegisterViewModels(ServiceCollection serviceCollection)
		{
			serviceCollection.AddTransient<XmlControlViewModel>(provider =>
			{
				var xmlcontext = ActivatorUtilities.CreateInstance(provider, typeof(XmlContext));
				var xmlrepository = ActivatorUtilities.CreateInstance(provider, typeof(AsyncXmlRepository), xmlcontext);
				var service = ActivatorUtilities.CreateInstance(provider, typeof(XmlPointService), xmlrepository);
				return (XmlControlViewModel)ActivatorUtilities.CreateInstance(provider, typeof(XmlControlViewModel), service);
			});

			serviceCollection.AddTransient<JsonControlViewModel>(provider =>
			{
				var jsoncontext = ActivatorUtilities.CreateInstance(provider, typeof(JsonContext));
				var jsonrepository = ActivatorUtilities.CreateInstance(provider, typeof(AsyncJsonRepository), jsoncontext);
				var service = ActivatorUtilities.CreateInstance(provider, typeof(JsonPointService), jsonrepository);
				return (JsonControlViewModel)ActivatorUtilities.CreateInstance(provider, typeof(JsonControlViewModel), service);
			});
		}

		private ServiceCollection RegisterServices(ServiceCollection serviceCollection)
		{
			serviceCollection.AddTransient<IUnitOfWork, JsonContext>();
			serviceCollection.AddTransient<IUnitOfWork, XmlContext>();

			serviceCollection.AddTransient<IAsyncRepository, AsyncXmlRepository>();
			serviceCollection.AddTransient<IAsyncRepository, AsyncJsonRepository>();

			serviceCollection.AddTransient<IPointService, JsonPointService>();
			serviceCollection.AddTransient<IPointService, XmlPointService>();

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
