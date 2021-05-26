using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using LMS.API.Data;
using System.Management;

namespace LMS.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InfoController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        public IActionResult Get()
        {
            return Ok( "Value1, value2");
        }
        //public InfoController(ILogger<InfoController> logger, IConfiguration config, IAksa aksa)
        //{
        //   // _logger = logger;
        //   // _config = config;
        //    _aksa = aksa;
        //}

        //    [HttpGet]
        //    public IActionResult Get()
        //    {

        //        var dateitme = _aksa.GetBuildDateTime(); 
        //        return Ok(new
        //        {
        //            date = dateitme
        //        });
        //    } 
    }
}
