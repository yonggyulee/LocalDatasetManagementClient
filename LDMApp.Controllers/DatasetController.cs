using LDMApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LDMApp.Controllers
{
    [ApiController]
    public class DatasetController
    {
        private readonly IDatasetApi datasetApi;

        public DatasetController(IDatasetApi datasetApi)
        {
            this.datasetApi = datasetApi;
        }

        [HttpGet("/")]
        public async Task<ICollection<string>> Get()
        {
            return await datasetApi.Get();
        }

        [HttpGet("/create_dataset={id}")]
        public async Task<string> Create(string id)
        {
            return await datasetApi.Create(id);
        }

    }
}
