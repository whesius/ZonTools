using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZonTools.Shared;

namespace ZonTools.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ServiceController : ControllerBase
    {
        private static readonly WindowsService[] Services = new[]
        {
            new WindowsService() { Name = "A", Status = "Running" },
            new WindowsService() { Name = "B", Status = "Running" },
            new WindowsService() { Name = "C", Status = "Running" }
        };

        private readonly ILogger<ServerController> _logger;

        public ServiceController(ILogger<ServerController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IEnumerable<WindowsService> Pull([FromBody] Model model)
        {
            _logger?.LogInformation($"{nameof(ServiceController)}.{nameof(Pull)}");
            
            return Services.ToArray();
        }

        public class Model
        {
            public string Server { get; set; }
        }
    }
}
