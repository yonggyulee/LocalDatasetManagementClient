using LDMApp.Core.Models;
using Prism.Mvvm;

namespace LDMApp.Modules.Samples
{
    public class SampleDataItem : BindableBase
    {
        public SampleDataItem(Sample input)
        {
            SampleID = input.SampleID;
            DatasetID = input.DatasetID;
            SampleType = input.SampleType;
            Metadata = input.Metadata;
            ImageCount = input.ImageCount;
        }

        private int _sampleID;
        public int SampleID
        {
            get { return _sampleID; }
            set { SetProperty(ref _sampleID, value); }
        }

        private string _datasetID;
        public string DatasetID
        {
            get { return _datasetID; }
            set { SetProperty(ref _datasetID, value); }
        }

        private int _sampleType;
        public int SampleType
        {
            get { return _sampleType; }
            set { SetProperty(ref _sampleType, value); }
        }

        private string _metadata;
        public string Metadata
        {
            get { return _metadata; }
            set { SetProperty(ref _metadata, value); }
        }

        private int _imageCount;
        public int ImageCount
        {
            get { return _imageCount; }
            set { SetProperty(ref _imageCount, value); }
        }
    }
}
