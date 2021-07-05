﻿using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrismCoreLearn.ViewModels.Dialogs
{
    public class SuccessDialogViewModel:BindableBase,IDialogAware
    {
        private string _title = "Notification";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Message = parameters.GetValue<string>("message");
        }

        private DelegateCommand _closeDialogCommand;
        public DelegateCommand CloseDialogCommand =>
            _closeDialogCommand ?? (_closeDialogCommand = new DelegateCommand(ExecuteCloseDialogCommand));

        async void ExecuteCloseDialogCommand()
        {
            ButtonResult result = ButtonResult.Yes;
            await RaiseRequestClose(new DialogResult(result));
        }

        public virtual async Task RaiseRequestClose(IDialogResult dialogResult)
        {
            await Task.Delay(500);
            RequestClose?.Invoke(dialogResult);
        }
    }
}
