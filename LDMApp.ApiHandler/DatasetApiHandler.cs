using LDMApp.Core;
using LDMApp.Services.Interfaces;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LDMApp.ApiHandler
{
    public class DatasetApiHandler
    {
        private readonly IDatasetApi datasetApi;

        public DatasetApiHandler()
        {
        }

        public DatasetApiHandler(IDatasetApi datasetApi)
        {
            this.datasetApi = datasetApi;
        }

        public async Task<ICollection<string>> Get()
        {
            return await CreateHttpClient().Get();
        }

        public async Task<string> Create(string id)
        {
            var api = RestService.For<IDatasetApi>(ApiSettings.DatasetApiURL);

            return await CreateHttpClient().Create(id);
        }

        private IDatasetApi CreateHttpClient()
        {
            return RestService.For<IDatasetApi>(ApiSettings.DatasetApiURL);
        }
    }
}
