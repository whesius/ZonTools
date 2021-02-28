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
     
        private readonly ILogger<ServerController> _logger;

        public ServiceController(ILogger<ServerController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IEnumerable<WindowsService> Pull([FromBody] Model model)
        {
            _logger?.LogInformation($"{nameof(ServiceController)}.{nameof(Pull)}");

            var services = System.ServiceProcess
                .ServiceController
                .GetServices(model.Server)
                .Select((s) => new WindowsService() { 
                    ServiceName = s.ServiceName, 
                    DisplayName = s.DisplayName,
                    Status = s.Status,
                    StartType = s.StartType,
                    CanStop = s.CanStop,
                    CanShutdown = s.CanShutdown,
                    CanPauseAndContinue = s.CanPauseAndContinue
                });            
                

            return services.ToArray();
        }

        public class Model
        {
            public string Server { get; set; }
        }
    }
}
