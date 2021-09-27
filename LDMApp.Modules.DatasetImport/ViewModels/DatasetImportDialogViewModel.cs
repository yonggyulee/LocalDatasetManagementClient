using LDMApp.Controllers;
using LDMApp.Core;
using LDMApp.Core.Models;
using LDMApp.Services.Interfaces;
using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using Refit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Threading.Tasks;

namespace LDMApp.Modules.DatasetImport.ViewModels
{
    public class DatasetImportDialogViewModel : BindableBase, IDialogAware
    {
        private string _title = AppSettings.DatasetImportTitle;
        private readonly IDialogService dialogService;
        private readonly DatasetController datasetController;
        private readonly SamplesController samplesController;
        private readonly ImagesController imagesController;

        public event Action<IDialogResult> RequestClose;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _datasetID;
        public string DatasetID
        {
            get { return _datasetID; }
            set { SetProperty(ref _datasetID, value); }
        }

        private string _dataFileName;
        public string DataFileName
        {
            get { return _dataFileName; }
            set { SetProperty(ref _dataFileName, value); }
        }

        private string _imageDirectoryPath;
        public string ImageDirectoryPath
        {
            get { return _imageDirectoryPath; }
            set { SetProperty(ref _imageDirectoryPath, value); }
        }

        private string dataDirectoryPath;

        private ObservableCollection<Sample> _samples = new ObservableCollection<Sample>();
        public ObservableCollection<Sample> Samples
        {
            get { return _samples; }
            set { SetProperty(ref _samples, value); }
        }

        private DelegateCommand _showDatasetSelectDialogCMD;
        public DelegateCommand ShowDatasetSelectDialogCMD =>
            _showDatasetSelectDialogCMD ?? (_showDatasetSelectDialogCMD = new DelegateCommand(ShowDatasetSelectDialog));

        private DelegateCommand _createDatasetCMD;
        public DelegateCommand CreateDatasetCMD =>
            _createDatasetCMD ?? (_createDatasetCMD = new DelegateCommand(CreateDataset));

        private DelegateCommand _findJsonFileCMD;
        public DelegateCommand FindJsonFileCMD =>
            _findJsonFileCMD ?? (_findJsonFileCMD = new DelegateCommand(ExecuteFindJsonFileCMD));

        private DelegateCommand _InsertDataCMD;
        public DelegateCommand InsertDataCMD =>
            _InsertDataCMD ?? (_InsertDataCMD = new DelegateCommand(ExecuteInsertDataCMD));

        private DelegateCommand _cancleCMD;
        public DelegateCommand CancleCMD =>
            _cancleCMD ?? (_cancleCMD = new DelegateCommand(ExecuteCancleCMD));

        private DelegateCommand _findImageDirectoryCMD;
        public DelegateCommand FindImageDirectoryCMD =>
            _findImageDirectoryCMD ?? (_findImageDirectoryCMD = new DelegateCommand(ExecuteFindImageDirectoryCMD));

        public DatasetImportDialogViewModel(IDialogService dialogService, IDatasetApi datasetApi, ISamplesApi samplesApi, IImagesApi imagesApi)
        {
            this.dialogService = dialogService;
            datasetController = new DatasetController(datasetApi);
            samplesController = new SamplesController(samplesApi);
            imagesController = new ImagesController(imagesApi);
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

        }

        private void ExecuteCancleCMD()
        {
            RequestClose.Invoke(new DialogResult(ButtonResult.Cancel));
        }

        private async void ExecuteInsertDataCMD()
        {
            bool result = await postSamples();
            var dparams = new DialogParameters();
            dparams.Add("datasetImportResult", result);
            dparams.Add("createdDataset", DatasetID);
            RequestClose.Invoke(new DialogResult(ButtonResult.OK, dparams));
        }

        private async Task<bool> postSamples()
        {
            foreach (Sample s in Samples)
            {
                try
                {
                    Sample result = await samplesController.Post(DatasetID, s);
                    foreach (Image img in s.images)
                    {
                        if (!await uploadImageFile(img))
                        {
                            await samplesController.Delete(DatasetID, s.SampleID);
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(
                        $"Sample 데이터 전송 중 오류가 발생하였습니다.\nSample ID : {s.SampleID}\n{e}",
                        "알림", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }                
            }
            return true;
        }

        private async Task<bool> uploadImageFile(Image image)
        {
            string verifiedPath = Path.Combine(ImageDirectoryPath, pathToVerifiedPath(image.OriginalFilename));

            if (!new FileInfo(verifiedPath).Exists)
            {
                MessageBox.Show($"해당 파일이 존재하지 않습니다.\nFilePath : {verifiedPath}", "알림", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            using (FileStream stream = File.Open(verifiedPath, FileMode.Open))
            {
                var ImageFile = new StreamPart(stream, image.OriginalFilename, "image/jpg");
                try
                {
                    await imagesController.UploadImageFileAsync(DatasetID, image.ImageID, ImageFile);
                }
                catch(Exception e)
                {
                    MessageBox.Show($"이미지 파일 전송 중 오류가 발생하였습니다.\nImage ID : {image.ImageID}\n{e}", "알림", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }   
            }
            return true;
        }

        private void ExecuteFindImageDirectoryCMD()
        {
            CommonOpenFileDialog dlgOpenDirectory = new CommonOpenFileDialog();
            if(dataDirectoryPath != null)
            {
                dlgOpenDirectory.InitialDirectory = dataDirectoryPath;
            }
            dlgOpenDirectory.IsFolderPicker = true;

            if (dlgOpenDirectory.ShowDialog() == CommonFileDialogResult.Ok)
            {
                ImageDirectoryPath = dlgOpenDirectory.FileName;
            }
        }

        private void ExecuteFindJsonFileCMD()
        {
            OpenFileDialog dlgOpenFile = new OpenFileDialog();
            dlgOpenFile.Filter = "Text Files(*.json;*.txt)|*.json;*.txt|All files(*.*)|*.*";
            
            if(dlgOpenFile.ShowDialog() == true)
            {
                string path = dlgOpenFile.FileName;
                int idx = path.LastIndexOf('\\') + 1;
                DataFileName = path.Substring(idx);
                dataDirectoryPath = path.Substring(0, idx);
                var fileStream = dlgOpenFile.OpenFile();

                string jsonString = File.ReadAllText(dlgOpenFile.FileName);
                var slist = JsonSerializer.Deserialize<ICollection<Sample>>(jsonString);
                Samples = new ObservableCollection<Sample>(slist);
            }
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
                    DatasetID = r.Parameters.GetValue<string>("selectedDataset");
                }
            });
        }

        private async void CreateDataset()
        {
            if(DatasetID == null || DatasetID.Equals(""))
            {
                MessageBox.Show("데이터셋 ID를 입력해주세요.", "알림", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            string result = await datasetController.Create(DatasetID);
            if (result.Equals(""))
            {
                MessageBox.Show("해당 데이터셋 ID는 이미 존재합니다.", "알림", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show($"데이터셋이 생성되었습니다.\nDatasetID : {result}", "알림", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private string pathToVerifiedPath(string path)
        {
            return path.Replace('/', '\\');
        }
    }
}
