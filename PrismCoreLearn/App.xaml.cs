using ModuleConfig;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using PrismCoreLearn.ViewModels.Dialogs;
using PrismCoreLearn.Views;
using PrismCoreLearn.Views.Dialogs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PrismCoreLearn
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()                                                     
        {
            return Container.Resolve<LoginWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //注册对话框
            containerRegistry.RegisterDialog<AlertDialog, AlertDialogViewModel>();
            containerRegistry.RegisterDialog<SuccessDialog, SuccessDialogViewModel>();
            containerRegistry.RegisterDialog<WarningDialog, WarningDialogViewModel>();

            //注册导航
            containerRegistry.RegisterForNavigation<LoginMain>();
            containerRegistry.RegisterForNavigation<CreateAccount>();
        }
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            //(代码方式)Code
            moduleCatalog.AddModule<ModuleA>();

            ////将ModuleB模块设置为按需加载
            var moduleBType = typeof(ModuleB);
            moduleCatalog.AddModule(new ModuleInfo()
            {
                ModuleName = moduleBType.Name,
                ModuleType = moduleBType.AssemblyQualifiedName,
                InitializationMode = InitializationMode.OnDemand
            });
        }

        /*
        public IContainerProvider Container => _containerExtension;

        protected virtual void Initialize()
        {
            ContainerLocator.SetContainerExtension(CreateContainerExtension);
            _containerExtension = ContainerLocator.Current;
            _moduleCatalog = CreateModuleCatalog();
            RegisterRequiredTypes(_containerExtension);
            RegisterTypes(_containerExtension);
            _containerExtension.FinalizeExtension();
            ConfigureModuleCatalog(_moduleCatalog);
            RegionAdapterMappings regionAdapterMappings = _containerExtension.Resolve<RegionAdapterMappings>();
            ConfigureRegionAdapterMappings(regionAdapterMappings);
            IRegionBehaviorFactory regionBehaviors = _containerExtension.Resolve<IRegionBehaviorFactory>();
            ConfigureDefaultRegionBehaviors(regionBehaviors);
            RegisterFrameworkExceptionTypes();
            Window window = CreateShell();
            if (window != null)
            {
                MvvmHelpers.AutowireViewModel(window);
                RegionManager.SetRegionManager(window, _containerExtension.Resolve<IRegionManager>());
                RegionManager.UpdateRegions();
                InitializeShell(window);
            }
            InitializeModules();
        }
        */
    }
}
