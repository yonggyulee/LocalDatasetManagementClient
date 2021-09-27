using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDMApp.Services.Interfaces
{
    public interface IDatasetApi
    {
        [Get("/")]
        Task<ICollection<string>> Get();

        [Get("/create_dataset={id}")]
        Task<string> Create(string id);
    }
}
