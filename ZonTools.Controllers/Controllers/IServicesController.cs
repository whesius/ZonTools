using System.Collections.Generic;
using System.Threading.Tasks;
using ZonTools.Shared;

namespace ZonTools.Controllers
{
    public interface IServicesController
    {
        Task<IEnumerable<WindowsService>> Get(string server);

    }
}