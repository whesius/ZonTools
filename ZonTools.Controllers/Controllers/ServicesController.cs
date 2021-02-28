using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ZonTools.Shared;

namespace ZonTools.Controllers
{
    public class ServicesController : IServicesController
    {
        private readonly IHttpClientFactory _clientFactory;
        public ServicesController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IEnumerable<WindowsService>> Get(string server)
        {
            var client = _clientFactory.CreateClient("ZonTools");
         
            var content = new { Server = server };

            var json = JsonSerializer.Serialize(content);
            var result = await client.PostAsync(
                new Uri("Service/Pull", UriKind.Relative),
                new StringContent(json, Encoding.UTF8, "application/json"));
                       
            if (result.IsSuccessStatusCode)
            {
                var resultContent = await result.Content.ReadAsAsync<WindowsService[]>();

                return resultContent;
            }

            return null;
        }       
    }
}
