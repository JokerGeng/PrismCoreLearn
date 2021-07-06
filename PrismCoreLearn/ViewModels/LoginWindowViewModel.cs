using Prism.Commands;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrismCoreLearn.ViewModels
{
    public class LoginWindowViewModel:BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IDialogService _dialogService;
        private readonly IModuleManager _moduleManager;

        public LoginWindowViewModel(IRegionManager regionManager, IDialogService dialogService, IModuleManager moduleManager)
        {
            _regionManager = regionManager;
            _dialogService = dialogService;
            _moduleManager = moduleManager;
        }

        private DelegateCommand _loginLoadingCommand;
        public DelegateCommand LoginLoadingCommand =>
            _loginLoadingCommand ?? (_loginLoadingCommand = new DelegateCommand(ExecuteLoginLoadingCommand));


        void ExecuteLoginLoadingCommand()
        {
            IRegion region = _regionManager.Regions[RegionNames.LoginContentRegion];
            region.RequestNavigate(NavigationNames.LoginMain, NavigationCompelted);
        }

        private void NavigationCompelted(NavigationResult result)
        {
            if (result.Result == true)
            {
                _dialogService.Show("SuccessDialog", new DialogParameters($"message={"导航到LoginMain页面成功"}"), null);
            }
            else
            {
                _dialogService.Show("WarningDialog", new DialogParameters($"message={"导航到LoginMain页面失败"}"), null);
            }
        }

        private DelegateCommand _loadModuleCommand;
        public DelegateCommand LoadModuleCommand =>
            _loadModuleCommand ?? (_loadModuleCommand = new DelegateCommand(ExecuteLoadModuleCommand));


        void ExecuteLoadModuleCommand()
        {
            _moduleManager.LoadModule("ModuleB");
        }
    }
}
