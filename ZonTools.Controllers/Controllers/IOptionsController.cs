using System.Collections.Generic;
using System.Threading.Tasks;
using ZonTools.Shared;

namespace ZonTools.Controllers
{
    public interface IOptionsController
    {
        Task<IEnumerable<WindowsServer>> GetServers();
    }
}