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

namespace LDMApp.Controllers
{
    [ApiController]
    public class ImagesController
    {
        private readonly IImagesApi imagesApi;

        public ImagesController(IImagesApi imagesApi)
        {
            this.imagesApi = imagesApi;
        }

        [HttpGet("/{dataset_id}")]
        public async Task<ICollection<Image>> GetList(string dataset_id)
        {
            return await imagesApi.GetList(dataset_id);
        }

        [HttpGet("/{dataset_id}/{sameple_id}")]
        public async Task<ICollection<Image>> GetList(string dataset_id, int sample_id)
        {
            return await imagesApi.GetList(dataset_id, sample_id);
        }

        [HttpGet("/{dataset_id}/file/image={id}")]
        public async Task<MemoryStream> GetImage(string dataset_id, string id)
        {
            HttpContent httpContent = await imagesApi.GetImage(dataset_id, id);
            byte[] bytes = await httpContent.ReadAsByteArrayAsync();
            MemoryStream memory = new MemoryStream(bytes);
            return memory;
        }

        [HttpPost("/{dataset_id}/upload")]
        public async Task<Image> UploadImage(string dataset_id, Image image)
        {
            return await imagesApi.UploadImage(dataset_id, image);
        }

        [HttpPost("/{dataset_id}/upload_image/{id}")]
        public async Task<string> UploadImageFileAsync(string dataset_id, string id, StreamPart imageFile)
        {
            var msg = await imagesApi.UploadImageFileAsync(dataset_id, id, imageFile);
            return await msg.Content.ReadAsStringAsync();
        }

        //[HttpPost("/{dataset_id}/upload_image/{id}")]
        //public string UploadImageFile(string dataset_id, string id, StreamPart imageFile)
        //{
        //    return imagesApi.UploadImageFile(dataset_id, id, imageFile);
        //}
    }
}
