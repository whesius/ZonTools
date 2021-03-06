using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using ZonTools.Shared;

namespace ZonTools.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [ApiVersion("1.1")]
    [ApiVersion("1.2")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class ServerController : ControllerBase
    {
        private static readonly WindowsServer[] Servers = new[]
        {
            new WindowsServer() { Name= ""},
            new WindowsServer() { Name=  System.Environment.MachineName }
        };

        private readonly ILogger<ServerController> _logger;

        public ServerController(ILogger<ServerController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<WindowsServer>> Get()
        {
            _logger?.LogInformation($"{nameof(ServerController)}.{nameof(Get)}");

            return Ok(Servers.ToArray());
        }
    }
}
