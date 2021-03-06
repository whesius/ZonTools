using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ZonTools.Shared;

namespace ZonTools.Controllers
{
    public class OptionsController : IOptionsController
    {
        private readonly IHttpClientFactory _clientFactory;
        public OptionsController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
               
        public async Task<IEnumerable<WindowsServer>> GetServers()
        {
            var client = _clientFactory.CreateClient("ZonTools");

            var result = await client.GetAsync("Server/Pull");
            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadAsAsync<WindowsServer[]>();                 
            }
            
            return Array.Empty<WindowsServer>();
        }
    }
}
