using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZonTools.Shared;

namespace ZonTools.Controllers
{
    public interface IWebSiteController
    {
        Task<IEnumerable<WebSite>> Pull(string server);
    }
}