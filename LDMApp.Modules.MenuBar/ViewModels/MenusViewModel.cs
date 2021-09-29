using LDMApp.ApiHandler;
using LDMApp.Core.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System.Windows;

namespace LDMApp.Modules.MenuBar.ViewModels
{
    public class MenusViewModel : BindableBase
    {
        private DatasetApiHandler datasetController;
        private readonly IDialogService dialogService;
        private readonly IEventAggregator eventAggregator;
        private string _currentDataset;
        public string CurrentDataset
        {
            get { return _currentDataset; }
            set { SetProperty(ref _currentDataset, value); }
        }

        public DelegateCommand ShowDatasetSelectDialogCMD { get; }
        public DelegateCommand ShowDatasetImportDIalogCMD { get; }

        public MenusViewModel(IDialogService dialogService, IEventAggregator eventAggregator //, IDatasetApi datasetApi
            )
        {
            //datasetController = new DatasetApiHandler(datasetApi);
            datasetController = new DatasetApiHandler();
            this.dialogService = dialogService;
            this.eventAggregator = eventAggregator;
            ShowDatasetSelectDialogCMD = new DelegateCommand(ShowDatasetSelectDialog);
            ShowDatasetImportDIalogCMD = new DelegateCommand(ShowDatasetImportDialog);
        }

        private async void ShowDatasetSelectDialog()
        {
            var datasetList = await datasetController.Get();
            var dparams = new DialogParameters();
            dparams.Add("datasetList", datasetList);

            dialogService.ShowDialog("DatasetSelectDialog", dparams, r =>
            { 
                if (r.Result == ButtonResult.OK)
                {
                    CurrentDataset = r.Parameters.GetValue<string>("selectedDataset");
                    eventAggregator.GetEvent<DatasetSelectedEvent>().Publish(CurrentDataset);
                }
            });
        }

        private void ShowDatasetImportDialog()
        {
            dialogService.ShowDialog("DatasetImportDialog", r => 
            { 
                if (r.Result == ButtonResult.OK)
                {
                    if (r.Parameters.GetValue<bool>("datasetImportResult"))
                    {
                        string datasetID = r.Parameters.GetValue<string>("createdDataset");
                        MessageBox.Show($"데이터셋이 성공적으로 생성되었습니다.\nDataset ID : {datasetID}", "알림", MessageBoxButton.OK, MessageBoxImage.Information);
                        CurrentDataset = datasetID;
                        eventAggregator.GetEvent<DatasetSelectedEvent>().Publish(CurrentDataset);
                    }
                    else
                    {
                        string datasetID = r.Parameters.GetValue<string>("createdDataset");
                        MessageBox.Show($"데이터셋 생성을 실패하였습니다.\nDataset ID : {datasetID}", "알림", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            });
        }
    }
}
