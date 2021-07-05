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

            //ָ������ע��ҳ��
            //��ʽһ
            regionManage.RegisterViewWithRegion("ContentRegion", typeof(Content));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
           
        }
    }
}
