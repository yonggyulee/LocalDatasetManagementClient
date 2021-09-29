using System;

namespace LDMApp.Core
{
    public static class ApiSettings
    {
        public static string IpAddress = Environment.GetEnvironmentVariable("DATASETAPI_IP") ?? "192.168.100.124";
        public const string SamplesApiName = "samples";
        public const string DatasetsApiName = "dataset";
        public const string ImagesApiName = "images";
        public static string SamplesApiURL = $"http://{IpAddress}:5000/api/samples";
        public static string DatasetApiURL = $"http://{IpAddress}:5000/api/dataset";
        public static string ImagesApiURL = $"http://{IpAddress}:5000/api/images";
    }
}
