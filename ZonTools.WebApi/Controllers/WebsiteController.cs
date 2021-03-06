using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Web.Administration;
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
            return Ok( await Task.Run(() => GetWebSites($"IIS://{model.Server}/W3SVC")));
        }
      
        static IEnumerable<WebSite> GetWebSites(String Path)
        { 
            ServerManager IIS = new ServerManager();

            var websites = new List<WebSite>();

            foreach (var site in IIS.Sites)
            {
                websites.Add(new WebSite()
                {
                    Name = site.Name,
                    Identity = (int)site.Id,                   
                    PhysicalPath = site.Applications["/"].VirtualDirectories[0].PhysicalPath
                });
            }

            return websites;

            //DirectoryEntry IIsEntities = new DirectoryEntry(Path);

            //foreach (DirectoryEntry IIsEntity in IIsEntities.Children)
            //{
            //    if (IIsEntity.SchemaClassName == "IIsWebServer")
            //    {
            //        yield return new WebSite()
            //        {
            //            Identity = Convert.ToInt32(IIsEntity.Name),
            //            Name = IIsEntity.Properties["ServerComment"].Value.ToString(),
            //            PhysicalPath = GetPath(IIsEntity),
            //            Status = (ServerState)IIsEntity.Properties["ServerState"].Value
            //        };
            //    }
            //}
        }

        //static String GetPath(DirectoryEntry IIsWebServer)
        //{
        //    foreach (DirectoryEntry IIsEntity in IIsWebServer.Children)
        //    {
        //        if (IIsEntity.SchemaClassName == "IIsWebVirtualDir")
        //            return IIsEntity.Properties["Path"].Value.ToString();
        //    }
        //    return null;
        //}

        public class PayLoadServer
        {
            public string Server { get; set; }
        }
    }
}
