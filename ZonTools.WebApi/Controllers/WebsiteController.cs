using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using ZonTools.Shared;

namespace ZonTools.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [ApiVersion("1.1")]
    [ApiVersion("1.2")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class WebsiteController : ControllerBase
    {

        private readonly ILogger<WebsiteController> _logger;

        public WebsiteController(ILogger<WebsiteController> logger)
        {
            _logger = logger;
        }


        [HttpPost]
        public async Task<ActionResult<IEnumerable<WebSite>>> Pull([FromBody] PayLoadServer model)
        {
            return Ok( await Task.Run(() => GetSites($"IIS://{model.Server}/W3SVC")));
        }

        private IEnumerable<WebSite> GetSites(string path)
        {
            DirectoryEntry IIsEntities = new DirectoryEntry(path);

            return (from s in IIsEntities.Children.OfType<DirectoryEntry>()
                    where s.SchemaClassName == "IIsWebServer"
                    select new WebSite
                    {
                        Identity = Convert.ToInt32(s.Name),
                        Name = s.Properties["ServerComment"].Value.ToString(),
                        PhysicalPath = (from p in s.Children.OfType<DirectoryEntry>()
                                        where p.SchemaClassName == "IIsWebVirtualDir"
                                        select p.Properties["Path"].Value.ToString()).Single(),
                        Status = (ServerState)s.Properties["ServerState"].Value
                    }).ToArray();
        }

        public class PayLoadServer
        {
            public string Server { get; set; }
        }
    }
}
