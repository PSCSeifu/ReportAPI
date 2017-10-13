using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ReportAPI.Common;
using ReportAPI.Models;
using ReportAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportAPI.Controllers
{
    [EnableCors("AngularPolicy")]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        public readonly ProjectAppSettings _AppSettings;
        private IUserService _service;

        public UserController(IUserService service, IOptions<ProjectAppSettings> options)
        {
            _AppSettings = options.Value;
            _service = service;
        }

        public IActionResult Health()
        {
            return Ok($"API User - Health oK @{DateTime.Now}");
        }

        [HttpPost]
        public IActionResult Authenticate(string username, string password)
        {
            var authenticatedUser = _service.Authenticate(username, password);

            if (authenticatedUser != null)
            {                
                return Ok(authenticatedUser);
            }
            return NotFound();
        }


    }
}
