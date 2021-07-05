using ModuleConfig.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleConfig
{
    public class ModuleB : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManage = containerProvider.Resolve<IRegionManager>();

            //指定区域注册页面
            //方式一
            regionManage.RegisterViewWithRegion("ContentRegion", typeof(OtherContent));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}
