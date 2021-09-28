using LDMApp.Core.Models;
using LDMApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LDMApp.ApiHandler
{
    public class ImagesApiHandler
    {
        private readonly IImagesApi imagesApi;

        public ImagesApiHandler(IImagesApi imagesApi)
        {
            this.imagesApi = imagesApi;
        }

        public async Task<ICollection<Image>> GetList(string dataset_id)
        {
            return await imagesApi.GetList(dataset_id);
        }

        public async Task<ICollection<Image>> GetList(string dataset_id, int sample_id)
        {
            return await imagesApi.GetList(dataset_id, sample_id);
        }

        public async Task<MemoryStream> GetImage(string dataset_id, string id)
        {
            HttpContent httpContent = await imagesApi.GetImage(dataset_id, id);
            byte[] bytes = await httpContent.ReadAsByteArrayAsync();
            MemoryStream memory = new MemoryStream(bytes);
            return memory;
        }

        public async Task<Image> UploadImage(string dataset_id, Image image)
        {
            return await imagesApi.UploadImage(dataset_id, image);
        }

        public async Task<string> UploadImageFileAsync(string dataset_id, string id, StreamPart imageFile)
        {
            var msg = await imagesApi.UploadImageFileAsync(dataset_id, id, imageFile);
            return await msg.Content.ReadAsStringAsync();
        }
    }
}
