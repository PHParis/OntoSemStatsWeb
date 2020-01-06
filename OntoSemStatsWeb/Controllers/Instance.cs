using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OntoSemStatsLib;

namespace OntoSemStatsWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Instance : ControllerBase
    {   
        // content negotiation
        // https://docs.microsoft.com/fr-fr/aspnet/core/web-api/advanced/formatting?view=aspnetcore-3.1
        // https://docs.microsoft.com/fr-fr/aspnet/core/web-api/advanced/custom-formatters?view=aspnetcore-3.1
        [HttpGet]
        public async Task<IActionResult> Get(string endpoint)
        {
            var res = new SemStatsResult {Endpoint = endpoint};
            await Task<SemStatsResult>.Factory.StartNew(() => 
                    res.Get());
            if (!string.IsNullOrWhiteSpace(res.ErrorMessage))
            {
                return BadRequest(res.ErrorMessage);
            }
            return Ok(res);
        }
    }
}