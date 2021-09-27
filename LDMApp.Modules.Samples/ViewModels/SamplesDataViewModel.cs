using LDMApp.Controllers;
using LDMApp.Core.Events;
using LDMApp.Core.Models;
using LDMApp.Services.Interfaces;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LDMApp.Modules.Samples.ViewModels
{
    public class SamplesDataViewModel : BindableBase
    {
        private SamplesApiHandler samplesController;

        private ObservableCollection<Sample> _sampleItems = new ObservableCollection<Sample>();
        public ObservableCollection<Sample> SampleItems 
        {
            get { return _sampleItems; } 
            set { SetProperty(ref _sampleItems, value); } 
        }

        private string _currentDataset;

        private DelegateCommand<ICollection<Image>> _changedCommand;
        private Sample _currentPositionSampleItem;
        private readonly IEventAggregator eventAggregator;

        public DelegateCommand<ICollection<Image>> ChangedCommand
        {
            get { return _changedCommand; }
            set { SetProperty(ref _changedCommand, value); }
        }

        public SamplesDataViewModel(ISamplesApi samplesApi, IEventAggregator eventAggregator)
        {
            samplesController = new SamplesApiHandler(samplesApi);
            this.eventAggregator = eventAggregator;
            eventAggregator.GetEvent<DatasetSelectedEvent>().Subscribe(setData, ThreadOption.UIThread);
            //getMsg();
        }

        public Sample CurrentPositionSampleItem
        {
            get { return _currentPositionSampleItem; }
            set
            {
                if(SetProperty(ref _currentPositionSampleItem, value))
                {
                    if(_currentPositionSampleItem != null)
                    {
                        IDictionary<string, object> dict = new Dictionary<string, object>();

                        dict.Add("currentDataset", _currentDataset);
                        dict.Add("images", CurrentPositionSampleItem.images);

                        eventAggregator.GetEvent<SampleSelectedEvent>().Publish(dict);
                    }
                }
            }
        }

        private async void setData(string dataset_id)
        {
            _currentDataset = dataset_id;
            var result = await samplesController.GetList(dataset_id);
            SampleItems = new ObservableCollection<Sample>(result);
            CurrentPositionSampleItem = SampleItems.First();
        }
    }
}
