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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using NLog;
using NLog.Extensions.Logging;
using Demo.Net.Wpf.JsonPresenter.Services;
using Demo.Net.Wpf.XmlPresenter.Services;
using Demo.Net.Wpf.Shared.ViewModels;

namespace Demo.Net.WpfApp
{
    public partial class App : Application
    {
        public IServiceProvider? ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            DispatcherUnhandledException += OnDemoDispatcherUnhandledException;
            Initialize();
        }

        private void Initialize()
        {
            var serviceCollection = new ServiceCollection();

            RegisterLogger(serviceCollection);
            RegisterServices(serviceCollection);
            RegisterViewModels(serviceCollection);
            RegisterViews(serviceCollection);
            RegisterShell(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();

            MainWindow = ServiceProvider.GetService<Shell>();
            MainWindow?.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            LogManager.Shutdown();
        }

        private void RegisterViews(ServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IWorkspace, HomeView>();
            serviceCollection.AddTransient<IWorkspace, XmlView>();
            serviceCollection.AddTransient<IWorkspace, JsonView>();
        }

        private void RegisterLogger(ServiceCollection serviceCollection)
        {
            var configuratioroot = new ConfigurationBuilder().Build();

            var logger = LogManager.Setup()
                                   .SetupExtensions(ext => ext.RegisterConfigSettings(configuratioroot))
                                   .GetCurrentClassLogger();

            serviceCollection.AddLogging(configure =>
            {
                configure.ClearProviders();
                configure.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                configure.AddNLog(configuratioroot);
            });
        }

        private static void RegisterViewModels(ServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<HomeViewModel>();

            serviceCollection.AddTransient<MathServiceViewModel, ParabolaMathServiceViewModel>(provider => 
            {
                var mathservice = ActivatorUtilities.CreateInstance(provider, typeof(ParabolaMathService));
                return (ParabolaMathServiceViewModel)ActivatorUtilities.CreateInstance(provider, typeof(ParabolaMathServiceViewModel), mathservice);
            });

            serviceCollection.AddTransient<MathServiceViewModel, LogarithmMathServiceViewModel>(provider =>
            {
                var mathservice = ActivatorUtilities.CreateInstance(provider, typeof(LogarithmMathService));
                return (LogarithmMathServiceViewModel)ActivatorUtilities.CreateInstance(provider, typeof(LogarithmMathServiceViewModel), mathservice);
            });
            serviceCollection.AddTransient<MathServiceViewModel, TangentMathServiceViewModel>(provider =>
            {
                var mathservice = ActivatorUtilities.CreateInstance(provider, typeof(TangentMathService));
                return (TangentMathServiceViewModel)ActivatorUtilities.CreateInstance(provider, typeof(TangentMathServiceViewModel), mathservice);
            });
            serviceCollection.AddTransient<MathServiceViewModel, ExponentiationMathServiceViewModel>(provider =>
            {
                var mathservice = ActivatorUtilities.CreateInstance(provider, typeof(ExponentiationMathService));
                return (ExponentiationMathServiceViewModel)ActivatorUtilities.CreateInstance(provider, typeof(ExponentiationMathServiceViewModel), mathservice);
            });

            serviceCollection.AddTransient<XmlViewModel>(provider =>
            {
                var xmlcontext = ActivatorUtilities.CreateInstance(provider, typeof(XmlContext));
                var xmlrepository = ActivatorUtilities.CreateInstance(provider, typeof(AsyncXmlRepository), xmlcontext);
                var service = ActivatorUtilities.CreateInstance(provider, typeof(XmlPointService), xmlrepository);
                return (XmlViewModel)ActivatorUtilities.CreateInstance(provider, typeof(XmlViewModel), service);
            });

            serviceCollection.AddTransient<JsonViewModel>(provider =>
            {
                var jsoncontext = ActivatorUtilities.CreateInstance(provider, typeof(JsonContext));
                var jsonrepository = ActivatorUtilities.CreateInstance(provider, typeof(AsyncJsonRepository), jsoncontext);
                var service = ActivatorUtilities.CreateInstance(provider, typeof(JsonPointService), jsonrepository);
                return (JsonViewModel)ActivatorUtilities.CreateInstance(provider, typeof(JsonViewModel), service);
            });
        }

        private ServiceCollection RegisterServices(ServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IMathService, TangentMathService>();
            serviceCollection.AddTransient<IMathService, ParabolaMathService>();
            serviceCollection.AddTransient<IMathService, LogarithmMathService>();
            serviceCollection.AddTransient<IMathService, ExponentiationMathService>();

            serviceCollection.AddTransient<IJsonPathService, JsonPathService>();
            serviceCollection.AddTransient<IXmlPathService, XmlPathService>();

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

        private void OnDemoDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            var logger = LogManager.GetCurrentClassLogger();
            logger.Error(e);
        }
    }
}
