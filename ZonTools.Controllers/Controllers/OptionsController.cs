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

            var result = await client.PostAsync(
                new Uri("Server/Pull", UriKind.Relative),
                new StringContent(string.Empty, Encoding.UTF8, "application/json")
                );
            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadAsAsync<WindowsServer[]>();                 
            }
            
            return Array.Empty<WindowsServer>();
        }
    }
}
