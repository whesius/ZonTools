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
    [Route("[controller]/[action]")]
    [ApiController]
    [ApiVersion("1.1")]
    [ApiVersion("1.2")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class ServiceController : ControllerBase
    {
     
        private readonly ILogger<ServerController> _logger;

        public ServiceController(ILogger<ServerController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<WindowsService>>> Pull([FromBody] PayLoadServer model)
        {
            _logger?.LogInformation($"{nameof(ServiceController)}.{nameof(Pull)}");

            var services = await Task.Run(() => 
            {
                return System.ServiceProcess
                        .ServiceController
                        .GetServices(model.Server)
                        .Select((s) => new WindowsService()
                        {
                          ServiceName = s.ServiceName,
                          DisplayName = s.DisplayName,
                          Status = s.Status,
                          StartType = s.StartType,
                          CanStop = s.CanStop,
                          CanShutdown = s.CanShutdown,
                          CanPauseAndContinue = s.CanPauseAndContinue
                        });
            });
            
           
            return Ok(services.ToArray());
        }

        [HttpPost]
        public IEnumerable<WindowsService> Start([FromBody] PayLoadServices model2)
        {
            _logger?.LogInformation($"{nameof(ServiceController)}.{nameof(Pull)}");

            var services = System.ServiceProcess
                .ServiceController
                .GetServices(model2.Server)
                .Where(v => model2.WindowsServices.Any(s => string.Equals(s.ServiceName, v.ServiceName)))
                .ToArray();
              
            foreach (var service in services)
            {
               if (service.Status != System.ServiceProcess.ServiceControllerStatus.Running)
                {
                    service.Start();
                    service.WaitForStatus(System.ServiceProcess.ServiceControllerStatus.Running, TimeSpan.FromSeconds(10));
                    service.Refresh();
                }
            }

            var windowsServices = services
               .Select((s) => new WindowsService()
               {
                   ServiceName = s.ServiceName,
                   DisplayName = s.DisplayName,
                   Status = s.Status,
                   StartType = s.StartType,
                   CanStop = s.CanStop,
                   CanShutdown = s.CanShutdown,
                   CanPauseAndContinue = s.CanPauseAndContinue
               });

            return windowsServices.ToArray();
        }

        [HttpPost]
        public IEnumerable<WindowsService> Stop([FromBody] PayLoadServices model2)
        {
            _logger?.LogInformation($"{nameof(ServiceController)}.{nameof(Pull)}");

            var services = System.ServiceProcess
                .ServiceController
                .GetServices(model2.Server)
                .Where(v => model2.WindowsServices.Any(s => string.Equals(s.ServiceName, v.ServiceName)))
                .ToArray();

            foreach (var service in services)
            {
                if (service.Status != System.ServiceProcess.ServiceControllerStatus.Stopped)
                {
                    service.Stop();
                    service.WaitForStatus(System.ServiceProcess.ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(10));
                    service.Refresh();
                }
            }

            var windowsServices = services
               .Select((s) => new WindowsService()
               {
                   ServiceName = s.ServiceName,
                   DisplayName = s.DisplayName,
                   Status = s.Status,
                   StartType = s.StartType,
                   CanStop = s.CanStop,
                   CanShutdown = s.CanShutdown,
                   CanPauseAndContinue = s.CanPauseAndContinue
               });

            return windowsServices.ToArray();
        }


        public class PayLoadServer
        {
            public string Server { get; set; }
        }

        public class PayLoadServices
        {
            public string Server { get; set; }
            public IEnumerable<WindowsService> WindowsServices { get; set; }
        }
    }
}
