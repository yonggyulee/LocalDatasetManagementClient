using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LDMApp.Core.Models
{
    public class Sample
    {

        //private int _sampleID;
        [JsonPropertyName("sampleID")]
        public int SampleID { get; set; }
        //{
        //    get { return _sampleID; }
        //    set { SetProperty(ref _sampleID, value); }
        //}

        //private string _datasetID;
        [JsonPropertyName("datasetID")]
        public string DatasetID { get; set; }
        //{
        //    get { return _datasetID; }
        //    set { SetProperty(ref _datasetID, value); }
        //}

        //private int _sampleType;
        [JsonPropertyName("sampleType")]
        public int SampleType { get; set; }
        //{ 
        //    get { return _sampleType; }
        //    set { SetProperty(ref _sampleType, value); }
        //}

        private string _metadata;
        [JsonPropertyName("metadata")]
        public string Metadata 
        {
            get 
            {
                string jsondata = _metadata;
                try
                {
                    if(jsondata != null)
                    {
                        jsondata = JValue.Parse(_metadata).ToString(Formatting.Indented);
                    }
                }
                catch (JsonReaderException e)
                {
                    Console.WriteLine(e.Message);
                }
                return jsondata;
            }
            set { _metadata = value; } 
        }
        //{
        //    get { return _metadata; }
        //    set { SetProperty(ref _metadata, value); }
        //}

        //private int _imageCount;
        [JsonPropertyName("imageCount")]
        public int ImageCount { get; set; }
        //{
        //    get { return _imageCount; }
        //    set { SetProperty(ref _imageCount, value); }
        //}

        //private ICollection<Image> _images;
        [JsonPropertyName("images")]
        public ICollection<Image> images { get; set; }
        //{
        //    get { return _images; }
        //    set { SetProperty(ref _images, value); }
        //}
    }
}
