using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace PrismCoreLearn.ViewModels
{
    class CreateAccountViewModel : BindableBase, INavigationAware, IConfirmNavigationRequest
    {
        private readonly IRegionManager _regionManager;
        private readonly IDialogService _dialogService;
        private IRegionNavigationJournal _journal;

        public CreateAccountViewModel(IRegionManager regionManager, IDialogService dialogService)
        {
            _regionManager = regionManager;
            _dialogService = dialogService;
        }

        private string _registeredLoginId;
        public string RegisteredLoginId
        {
            get { return _registeredLoginId; }
            set { SetProperty(ref _registeredLoginId, value); }
        }

        public bool IsConfirmNavigationRequest { get; set; } = false;

        public string Passward { get; set; }
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            //MessageBox.Show("从LoginMainContent导航到CreateAccount");
            _journal = navigationContext.NavigationService.Journal;
        }

        private DelegateCommand _loginMainContentCommand;
        public DelegateCommand LoginMainContentCommand =>
            _loginMainContentCommand ?? (_loginMainContentCommand = new DelegateCommand(ExecuteLoginMainContentCommand));

        private DelegateCommand _goBackCommand;
        public DelegateCommand GoBackCommand =>
            _goBackCommand ?? (_goBackCommand = new DelegateCommand(ExecuteGoBackCommand));

        private DelegateCommand<object> _verityCommand;
        public DelegateCommand<object> VerityCommand =>
             _verityCommand ?? (_verityCommand = new DelegateCommand<object>(ExecuteVerityCommand));

        void ExecuteGoBackCommand()
        {
            if(_journal.CanGoBack)
            {
                _journal.GoBack();
            }
        }

        void ExecuteLoginMainContentCommand()
        {
            Navigate(NavigationNames.LoginMain);
        }

        void ExecuteVerityCommand(object parameter)
        {
            if (!VerityRegister(parameter))
            {
                return;
            }
            var Title = string.Empty;
            IsConfirmNavigationRequest = true;
            _dialogService.ShowDialog("SuccessDialog", new DialogParameters($"message={"注册成功"}"), null);
            _journal.GoBack();
        }

        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            if (!string.IsNullOrEmpty(RegisteredLoginId) && IsConfirmNavigationRequest)
            {
                _dialogService.ShowDialog("AlertDialog", new DialogParameters($"message={"是否需要用当前注册的用户登录?"}"), r =>
                {
                    if (r.Result == ButtonResult.Yes)
                    {
                        navigationContext.Parameters.Add("loginId", RegisteredLoginId);
                        navigationContext.Parameters.Add("passward", Passward);
                    }

                });
            }
            continuationCallback(true);
        }

        private bool VerityRegister(object parameter)
        {
            if (string.IsNullOrEmpty(this.RegisteredLoginId))
            {
                MessageBox.Show("LoginId 不能为空！");
                return false;
            }
            var passwords = parameter as Dictionary<string, PasswordBox>;
            var password = (passwords["Password"] as PasswordBox).Password;
            var confimPassword = (passwords["ConfirmPassword"] as PasswordBox).Password;
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Password 不能为空！");
                return false;
            }
            if (string.IsNullOrEmpty(confimPassword))
            {
                MessageBox.Show("ConfirmPassword 不能为空！");
                return false;
            }
            if (password.Trim() != confimPassword.Trim())
            {
                MessageBox.Show("两次密码不一致");
                return false;
            }
            Passward = password;
            //Global.AllUsers.Add(new User()
            //{
            //    Id = Global.AllUsers.Max(t => t.Id) + 1,
            //    LoginId = this.RegisteredLoginId,
            //    PassWord = password
            //});
            return true;
        }

        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
                _regionManager.RequestNavigate(RegionNames.LoginContentRegion, navigatePath);
        }
    }
}
