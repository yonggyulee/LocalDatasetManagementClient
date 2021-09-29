using LDMApp.Core;
using LDMApp.Services.Interfaces;
using Refit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LDMApp.Services
{
    public class DatasetRequestService
    {
        private readonly IHttpClientFactory httpClientFactory;

        public DatasetRequestService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<string> Get()
        {
            using (var client = httpClientFactory.CreateClient())
            {
                var Message = await client.GetAsync("/");
                using var Stream = await Message.Content.ReadAsStreamAsync();
                using var Reader = new StreamReader(Stream);
                return await Reader.ReadToEndAsync();
            }
        }
    }
}
