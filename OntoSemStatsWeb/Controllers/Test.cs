using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace OntoSemStatsWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Test : ControllerBase
    {   
        // content negotiation
        // https://docs.microsoft.com/fr-fr/aspnet/core/web-api/advanced/formatting?view=aspnetcore-3.1
        // https://docs.microsoft.com/fr-fr/aspnet/core/web-api/advanced/custom-formatters?view=aspnetcore-3.1
        [HttpGet]
        public IEnumerable<int> Get(string endpoint)
        {
            return new[] {1, 2, 3};
        }
    }
}