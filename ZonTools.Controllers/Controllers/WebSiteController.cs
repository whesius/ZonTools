using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ZonTools.Shared;

namespace ZonTools.Controllers
{
    public class WebSiteController : IWebSiteController
    {
        private readonly IHttpClientFactory _clientFactory;
        public WebSiteController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
               
        public async Task<IEnumerable<WebSite>> Pull(string server)
        {
            var client = _clientFactory.CreateClient("ZonTools");

            var content = new { Server = server };
            var json = JsonSerializer.Serialize(content);

            var result = await client.PostAsync(
                new Uri("WebSite/Pull", UriKind.Relative),
                new StringContent(json, Encoding.UTF8, "application/json")
                );
            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadAsAsync<WebSite[]>();                 
            }
            
            return Array.Empty<WebSite>();
        }
    }
}
