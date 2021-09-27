using LDMApp.Core;
using Prism.Mvvm;

namespace LDMApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = AppSettings.AppTitle;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel()
        {

        }
    }
}
