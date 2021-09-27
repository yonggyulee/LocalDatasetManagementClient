using LDMApp.Core;
using LDMApp.Module.Images.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace LDMApp.Module.Images
{
    public class ImagesModule : IModule
    {
        private readonly IRegionManager regionManager;

        public ImagesModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            var view = containerProvider.Resolve<ImagesData>();
            regionManager.Regions[RegionNames.ImagesRegion].Add(view);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}