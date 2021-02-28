using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZonTools.Controllers
{
    public interface IOptionsController
    {
        Task<IEnumerable<string>> GetServers();
    }
}