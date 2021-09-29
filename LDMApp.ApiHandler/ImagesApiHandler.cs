using LDMApp.Core;
using LDMApp.Core.Models;
using LDMApp.Services.Interfaces;
using Refit;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace LDMApp.ApiHandler
{
    public class ImagesApiHandler
    {
        //private readonly IImagesApi imageApi;

        public ImagesApiHandler()
        {

        }
        //public ImagesApiHandler(IImagesApi imagesApi)
        //{
        //    this.imageApi = imagesApi;
        //}

        public async Task<ICollection<Image>> GetList(string dataset_id)
        {
            return await CreateHttpClient().GetList(dataset_id);
        }

        public async Task<ICollection<Image>> GetList(string dataset_id, int sample_id)
        {
            return await CreateHttpClient().GetList(dataset_id, sample_id);
        }

        public async Task<MemoryStream> GetImage(string dataset_id, string id)
        {
            HttpContent httpContent = await CreateHttpClient().GetImage(dataset_id, id);
            byte[] bytes = await httpContent.ReadAsByteArrayAsync();
            MemoryStream memory = new MemoryStream(bytes);
            return memory;
        }

        public async Task<Image> UploadImage(string dataset_id, Image image)
        {
            return await CreateHttpClient().UploadImage(dataset_id, image);
        }

        public async Task<string> UploadImageFileAsync(string dataset_id, string id, StreamPart imageFile)
        {
            var msg = await CreateHttpClient().UploadImageFileAsync(dataset_id, id, imageFile);
            return await msg.Content.ReadAsStringAsync();
        }

        private IImagesApi CreateHttpClient()
        {
            return RestService.For<IImagesApi>(ApiSettings.ImagesApiURL);
        }
    }
}
