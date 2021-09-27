using LDMApp.Core;
using LDMApp.Modules.MenuBar.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace LDMApp.Modules.MenuBar
{
    public class MenuBarModule : IModule
    {
        private readonly IRegionManager regionManager;

        public MenuBarModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            var view = containerProvider.Resolve<Menus>();
            regionManager.Regions[RegionNames.MenubarRegion].Add(view);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}