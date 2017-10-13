using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TemplateAPI.Controllers
{
    [Route("api/[controller]")]
    public class EnvController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            var dict = new Dictionary<string, string>();

            var enumerator = Environment.GetEnvironmentVariables().GetEnumerator();
            while (enumerator.MoveNext())
            {
                dict.Add(enumerator.Key.ToString(), enumerator.Value.ToString());
            }

            return Ok(dict);
        }
    }
}
