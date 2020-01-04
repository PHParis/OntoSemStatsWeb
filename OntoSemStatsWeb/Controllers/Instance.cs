using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OntoSemStatsLib;
using VDS.RDF;

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
        public IActionResult Get(string endpoint)
        {
            var g = new Graph();
            g.Assert(
                g.CreateUriNode(new Uri("http://example.org/Test1")), 
                g.CreateUriNode(new Uri("http://example.org/get")), 
                g.CreateUriNode(new Uri("http://example.org/Test2")));
            var res = new SemStatsResult 
            {
                Date = DateTime.Now,
                ErrorMessage = null,
                Instance = g
            };
            return Ok(res);
        }
    }
}