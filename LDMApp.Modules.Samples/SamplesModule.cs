using LDMApp.Core;
using LDMApp.Modules.Samples.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace LDMApp.Modules.Samples
{
    public class SamplesModule : IModule
    {
        private readonly IRegionManager regionManager;

        public SamplesModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var view = containerProvider.Resolve<SamplesData>();
            regionManager.Regions[RegionNames.SamplesRegion].Add(view);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}