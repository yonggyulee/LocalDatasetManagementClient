using LDMApp.Core;
using LDMApp.Core.Models;
using LDMApp.Services.Interfaces;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LDMApp.ApiHandler
{
    public class SamplesApiHandler
    {
        //private readonly ISamplesApi samplesApi;

        public SamplesApiHandler()
        {

        }

        //public SamplesApiHandler(ISamplesApi samplesApi)
        //{
        //    this.samplesApi = samplesApi;
        //}

        public async Task<ICollection<Sample>> GetList(string dataset_id)
        {
            var result = await CreateHttpClient().GetList(dataset_id);
            return result;
        }

        public async Task<Sample> Post(string dataset_id, Sample sample) 
        {
            return await CreateHttpClient().Post(dataset_id, sample);
        }

        public async Task<string> Delete(string dataset_id, int id)
        {
            return await CreateHttpClient().Delete(dataset_id, id);
        }

        private ISamplesApi CreateHttpClient()
        {
            return RestService.For<ISamplesApi>(ApiSettings.SamplesApiURL);
        }
    }
}
