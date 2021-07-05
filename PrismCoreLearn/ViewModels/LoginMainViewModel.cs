using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using PrismCoreLearn.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace PrismCoreLearn.ViewModels
{
    public class LoginMainViewModel : BindableBase, INavigationAware
    {
        private IRegionNavigationJournal _journal;
        private readonly IRegionManager _regionManager;
        private readonly IDialogService _dialogService;

        private bool _isCanExcute;
        public bool IsCanExcute
        {
            get { return _isCanExcute; }
            set { SetProperty(ref _isCanExcute, value); }
        }

        private UserInfo _currentUser = new UserInfo();
        public UserInfo CurrentUser
        {
            get { return _currentUser; }
            set { SetProperty(ref _currentUser, value); }
        }
        public LoginMainViewModel(IRegionManager regionManager, IDialogService dialogService)
        {
            _regionManager = regionManager;
            _dialogService = dialogService;
        }
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            //MessageBox.Show("从CreateAccount导航到LoginMainContent");
            _journal = navigationContext.NavigationService.Journal;

            var loginId = navigationContext.Parameters["loginId"] as string;
            var passWord = navigationContext.Parameters["passWard"] as string;
            if (loginId != null)
            {
                this.CurrentUser = new UserInfo() { LoginId = loginId,PassWord= passWord };
            }
            LoginCommand.RaiseCanExecuteChanged();
        }

        private DelegateCommand _createAccountCommand;
        public DelegateCommand CreateAccountCommand =>
            _createAccountCommand ?? (_createAccountCommand = new DelegateCommand(ExecuteCreateAccountCommand));

        private DelegateCommand _goForwardCommand;
        public DelegateCommand GoForwardCommand =>
            _goForwardCommand ?? (_goForwardCommand = new DelegateCommand(ExecuteGoForwardCommand));

        private DelegateCommand<PasswordBox> _loginCommand;
        public DelegateCommand<PasswordBox> LoginCommand =>
            _loginCommand ?? (_loginCommand = new DelegateCommand<PasswordBox>(ExecuteLoginCommand, CanExecuteGoForwardCommand));

        void ExecuteCreateAccountCommand()
        {
            Navigate(NavigationNames.CreateAccount);
        }

        void ExecuteLoginCommand(PasswordBox passwordBox)
        {
            if (string.IsNullOrEmpty(this.CurrentUser.LoginId))
            {
                _dialogService.Show("WarningDialog", new DialogParameters($"message={"LoginId 不能为空!"}"), null);
                return;
            }
            this.CurrentUser.PassWord = passwordBox.Password;
            if (string.IsNullOrEmpty(this.CurrentUser.PassWord))
            {
                _dialogService.Show("WarningDialog", new DialogParameters($"message={"PassWord 不能为空!"}"), null);
                return;
            }
            //else if (Global.AllUsers.Where(t => t.LoginId == this.CurrentUser.LoginId && t.PassWord == this.CurrentUser.PassWord).Count() == 0)
            //{
            //    _dialogService.Show("WarningDialog", new DialogParameters($"message={"LoginId 或者 PassWord 错误!"}"), null);
            //    return;
            //}
            //ShellSwitcher.Switch<LoginWindow, MainWindow>();
        }

        private void ExecuteGoForwardCommand()
        {
            if(_journal.CanGoForward)
            {
                _journal.GoForward();
            }
        }

        private bool CanExecuteGoForwardCommand(PasswordBox passwordBox)
        {
            this.IsCanExcute = _journal != null && _journal.CanGoForward;
            return true;
        }

        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
                _regionManager.RequestNavigate(RegionNames.LoginContentRegion, navigatePath);
        }
    }
}
