using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace LDMApp.Dialogs
{
    class DataTransferPbDialogViewModel : BindableBase, IDialogAware
    {
        public string Title => "Data 전송";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            throw new NotImplementedException();
        }

        public void OnDialogClosed()
        {
            throw new NotImplementedException();
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            throw new NotImplementedException();
        }
    }
}
