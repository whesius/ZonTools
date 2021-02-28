using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ZonTools.Controllers
{
    public class OptionsController : IOptionsController
    {
        private readonly IHttpClientFactory _clientFactory;
        public OptionsController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IEnumerable<string>> GetServers()
        {
            var client = _clientFactory.CreateClient("ZonTools");

            var result = await client.GetAsync("Server");
            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadAsAsync<string[]>();                 
            }
            
            return null;
        }
    }
}
