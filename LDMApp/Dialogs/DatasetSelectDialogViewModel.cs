using LDMApp.Core;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LDMApp.Dialogs
{
    public class DatasetSelectDialogViewModel : BindableBase, IDialogAware
    {
        public string Title => AppSettings.DatasetSelectDialogTitle;

        private ObservableCollection<string> _datasetItems = new ObservableCollection<string>();
        public ObservableCollection<string> DatasetItems
        {
            get { return _datasetItems; }
            set { SetProperty(ref _datasetItems, value); }
        }

        private string _selectedDataset;
        public string SelectedDataset
        {
            get { return _selectedDataset; }
            set { SetProperty(ref _selectedDataset, value); }
        }

        public event Action<IDialogResult> RequestClose;
        public DelegateCommand<string> CloseDialogCommand { get; }
        public DelegateCommand<string> OKCommand { get; }

        public DatasetSelectDialogViewModel()
        {
            CloseDialogCommand = new DelegateCommand<string>(CloseDialog);
            OKCommand = new DelegateCommand<string>(OKCloseDialog);
        }

        private void CloseDialog(string parameter)
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
        }

        private void OKCloseDialog(string parameter)
        {
            var dparams = new DialogParameters();
            dparams.Add("selectedDataset", SelectedDataset);
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK, dparams));
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            var dataList = parameters.GetValue<ICollection<string>>("datasetList");
            if (dataList != null)
            {
                DatasetItems = new ObservableCollection<string>(dataList);
            }
        }
    }
}
