using LDMApp.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LDMApp.Services.Interfaces
{
    public interface ISamplesApi
    {
        [Get("/{dataset_id}")]
        Task<ICollection<Sample>> GetList(string dataset_id);

        [Get("/{dataset_id}/{id}")]
        Task<Sample> Get(string dataset_id, int id);

        [Post("/{dataset_id}")]
        Task<Sample> Post(string dataset_id, [Body] Sample sample);

        [Delete("/{dataset_id}/{id}")]
        Task<string> Delete(string dataset_id, int id);

        [Put("/{dataset_id}/{id}")]
        Task<string> Put(string dataset_id, string id, [Body] Sample sample);
    }
}
