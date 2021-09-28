using LDMApp.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LDMApp.Services.Interfaces
{
    public interface IImagesApi
    {
        [Get("/{dataset_id}")]
        Task<ICollection<Image>> GetList(string dataset_id);

        [Get("/{dataset_id}/sample={id}")]
        Task<ICollection<Image>> GetList(string dataset_id, int id);

        [Get("/{dataset_id}/{id}")]
        Task<Image> Get(string dataset_id, string id);

        [Get("/{dataset_id}/file/{id}")]
        Task<HttpContent> GetImage(string dataset_id, string id);

        [Post("/{dataset_id}")]
        Task<Image> Post(string dataset_id, [Body] Image image);

        [Delete("/{dataset_id}/{id}")]
        Task<string> Delete(string dataset_id, string id);

        [Put("/{dataset_id}/{id}")]
        Task<string> Put(string dataset_id, string id, [Body] Image image);

        [Post("/{dataset_id}/upload")]
        Task<Image> UploadImage(string dataset_id, [Body(BodySerializationMethod.UrlEncoded)] Image image);

        [Multipart]
        [Post("/{dataset_id}/upload/{id}")]
        Task<HttpResponseMessage> UploadImageFileAsync(string dataset_id, string id, [AliasAs("imageFile")] StreamPart imageFile);

        //[Multipart]
        //[Post("/{dataset_id}/upload{id}")]
        //string UploadImageFile(string dataset_id, string id, [AliasAs("imageFile")] StreamPart imageFile);
    }
}
