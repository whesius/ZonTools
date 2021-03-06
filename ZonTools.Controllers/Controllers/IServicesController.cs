using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZonTools.Shared;

namespace ZonTools.Controllers
{
    public interface IServicesController
    {
        Task<IEnumerable<WindowsService>> Pull(string server);

        Task<IEnumerable<WindowsService>> Start(string server, IEnumerable<WindowsService> services);

        Task<IEnumerable<WindowsService>> Stop(string server, IEnumerable<WindowsService> services);

    }
}