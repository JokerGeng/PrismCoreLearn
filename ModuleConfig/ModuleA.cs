using ModuleConfig.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;

namespace ModuleConfig
{
    public class ModuleA : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManage = containerProvider.Resolve<IRegionManager>();

            //指定区域注册页面
            //方式一
            regionManage.RegisterViewWithRegion("ContentRegion", typeof(Content));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
           
        }
    }
}
