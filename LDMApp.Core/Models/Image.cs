using Prism.Mvvm;
using Refit;
using System.Text.Json.Serialization;
using System.Windows.Media.Imaging;

namespace LDMApp.Core.Models
{
    public class Image : BindableBase
    {
        private string _imageID;
        [JsonPropertyName("imageID")]
        public string ImageID 
        {   
            get { return _imageID; }
            set { SetProperty(ref _imageID, value); }
        }

        private int _sampleID;
        [JsonPropertyName("sampleID")]
        public int SampleID
        {
            get { return _sampleID; }
            set { SetProperty(ref _sampleID, value); }
        }

        private int _imageNO;
        [JsonPropertyName("imageNO")]
        public int ImageNO
        {
            get { return _imageNO; }
            set { SetProperty(ref _imageNO, value); }
        }

        private string _imageCode;
        [JsonPropertyName("imageCode")]
        public string ImageCode
        {
            get { return _imageCode; }
            set { SetProperty(ref _imageCode, value); }
        }

        private string _originalFilename;
        [JsonPropertyName("originalFilename")]
        public string OriginalFilename
        {
            get { return _originalFilename; }
            set { SetProperty(ref _originalFilename, value); }
        }

        private string _imageScheme;
        [JsonPropertyName("imageScheme")]
        public string ImageScheme
        {
            get { return _imageScheme; }
            set { SetProperty(ref _imageScheme, value); }
        }

        [AliasAs("imageFile")]
        public StreamPart ImageFile { get; set; }

        private BitmapImage _bitmapImage;
        public BitmapImage BitmapImage
        {
            get { return _bitmapImage; }
            set { SetProperty(ref _bitmapImage, value); }
        }

        public override string ToString()
        {
            return $"Image: ({ImageID}, {SampleID}, {ImageNO}, {ImageCode}, {OriginalFilename}, {ImageScheme})";
        }
    }
}
