using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace X5on.Core.Controllers
{
    public class HomeController
    {

        [HttpGet("/hello")]
        public string hello()
        {
            return "Hello world";
        }


    }
}
