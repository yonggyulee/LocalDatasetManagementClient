using LDMApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LDMApp.Controllers
{
    public class DatasetApiHandler
    {
        private readonly IDatasetApi datasetApi;

        public DatasetApiHandler(IDatasetApi datasetApi)
        {
            this.datasetApi = datasetApi;
        }

        public async Task<ICollection<string>> Get()
        {
            return await datasetApi.Get();
        }

        public async Task<string> Create(string id)
        {
            return await datasetApi.Create(id);
        }

    }
}
