using LDMApp.Controllers;
using LDMApp.Core.Events;
using LDMApp.Core.Models;
using LDMApp.Services.Interfaces;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Windows.Media.Imaging;

namespace LDMApp.Module.Images.ViewModels
{
    public class ImagesDataViewModel : BindableBase
    {
        private ImagesApiHandler imagesController;

        private ObservableCollection<Image> _imageItems = new ObservableCollection<Image>();
        
        public ObservableCollection<Image> ImageItems
        {
            get { return _imageItems; }
            set { SetProperty(ref _imageItems, value); }
        }

        private Image _currentPositionImageItem;
        private readonly IEventAggregator eventAggregator;

        public Image CurrentPositionImageItem
        {
            get { return _currentPositionImageItem; }
            set { SetProperty(ref _currentPositionImageItem, value); }
        }

        public ImagesDataViewModel(IImagesApi imagesApi, IEventAggregator eventAggregator)
        {
            imagesController = new ImagesApiHandler(imagesApi);
            this.eventAggregator = eventAggregator;
            eventAggregator.GetEvent<SampleSelectedEvent>().Subscribe(OnSampleSelected, ThreadOption.UIThread);
        }

        private async void OnSampleSelected(IDictionary<string, object> dict)
        {
            string currentDataset = (string)dict["currentDataset"];
            ICollection<Image> images = (ICollection<Image>)dict["images"];
            this.ImageItems = new ObservableCollection<Image>(images);
            if(ImageItems.Count > 0)
            {
                CurrentPositionImageItem = ImageItems.First();
                foreach(Image i in ImageItems)
                {
                    var stream = await imagesController.GetImage(currentDataset, i.ImageID);
                    using (stream)
                    {
                        if(stream.Length > 0)
                        {
                            BitmapImage bitimg = new BitmapImage();
                            bitimg.BeginInit();
                            bitimg.CacheOption = BitmapCacheOption.OnLoad;
                            bitimg.StreamSource = stream;
                            bitimg.EndInit();
                            bitimg.Freeze();
                            var item = ImageItems.Where(img => img.ImageID == i.ImageID)
                                      .FirstOrDefault()
                                      ;
                            if (item != null)
                            {
                                item.BitmapImage = bitimg;
                            }
                        }
                    }
                }
            }
        }
    }
}
