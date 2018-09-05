using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Image.Common.Log;
using Microsoft.AspNetCore.Mvc;

namespace Image.Server.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly ILog _logger;
        public HomeController(ILog logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public string Index()
        {
            return "hello world";
        }
    }
}
