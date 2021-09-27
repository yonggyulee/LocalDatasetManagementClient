using DryIoc.Microsoft.DependencyInjection.Extension;
using LDMApp.Views;
using Prism.DryIoc;
using Prism.Ioc;
using Refit;
using System.Windows;
using LDMApp.Services.Interfaces;
using LDMApp.Core;
using LDMApp.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using Prism.Modularity;
using LDMApp.Modules.MenuBar;
using LDMApp.Modules.Samples;
using LDMApp.Module.Images;
using LDMApp.Dialogs;
using LDMApp.Modules.DatasetImport.Views;
using LDMApp.Modules.DatasetImport;
using LDMApp.Modules.DatasetImport.ViewModels;

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
}
