using DryIoc.Microsoft.DependencyInjection.Extension;
using LDMApp.Core;
using LDMApp.Dialogs;
using LDMApp.Module.Images;
using LDMApp.Modules.DatasetImport;
using LDMApp.Modules.DatasetImport.ViewModels;
using LDMApp.Modules.DatasetImport.Views;
using LDMApp.Modules.MenuBar;
using LDMApp.Modules.Samples;
using LDMApp.Services.Interfaces;
using LDMApp.Views;
using Microsoft.Extensions.DependencyInjection;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using Refit;
using System;
using System.Windows;

namespace LDMApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        public static ServiceControl GetServiceControl()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddRefitClient<IDatasetApi>()
                        .ConfigureHttpClient(c => c.BaseAddress = new Uri(ApiSettings.DatasetApiURL));
            services.AddRefitClient<ISamplesApi>()
                        .ConfigureHttpClient(c => c.BaseAddress = new Uri(ApiSettings.SamplesApiURL));
            services.AddRefitClient<IImagesApi>()
                    .ConfigureHttpClient(c => c.BaseAddress = new Uri(ApiSettings.ImagesApiURL));
            var serviceProvider = services.BuildServiceProvider();

            return new ServiceControl(serviceProvider);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.GetContainer().RegisterServices(services =>
            {
                services.AddRefitClient<IDatasetApi>()
                        .ConfigureHttpClient(c => c.BaseAddress = new Uri(ApiSettings.DatasetApiURL));
                services.AddRefitClient<ISamplesApi>()
                        .ConfigureHttpClient(c => c.BaseAddress = new Uri(ApiSettings.SamplesApiURL));
                services.AddRefitClient<IImagesApi>()
                        .ConfigureHttpClient(c => c.BaseAddress = new Uri(ApiSettings.ImagesApiURL));
            });
            containerRegistry.RegisterDialog<DatasetSelectDialog, DatasetSelectDialogViewModel>();
            containerRegistry.RegisterDialog<DatasetImportDialog, DatasetImportDialogViewModel>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<MenuBarModule>();
            moduleCatalog.AddModule<SamplesModule>();
            moduleCatalog.AddModule<ImagesModule>();
            moduleCatalog.AddModule<DatasetImportModule>();
        }
    }

    public class ServiceControl
    {
        public ServiceProvider SvcProvider { get; private set; }

        public ServiceControl(ServiceProvider serviceProvider)
        {
            this.SvcProvider
            = serviceProvider;
        }
    }
}
