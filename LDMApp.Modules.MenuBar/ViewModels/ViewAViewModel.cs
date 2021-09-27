using LDMApp.Controllers;
using LDMApp.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDMApp.Modules.MenuBar.ViewModels
{
    public class ViewAViewModel : BindableBase
    {
        private DatasetController datasetController;

        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public ViewAViewModel(IDatasetApi datasetApi)
        {
            Message = "ViewA";
            datasetController = new DatasetController(datasetApi);
            getMsg();
        }

        private async void getMsg()
        {
            try
            {
                var result = await datasetController.Get();
                //if (result != null) Message = result;
            }
            catch (Exception e)
            {
                Message = e.ToString();
            }
        }
    }
}
