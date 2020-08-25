using System;
using Microsoft.AspNetCore.Mvc;

namespace DanceFlow.Controllers {
    [Route("applications")]
    public class ApplicationsController : Controller {
        // GET
        [HttpGet("")]
        public IActionResult AllApplications()
        {
            return View();
        }

        [HttpPost("allocate")]
        public IActionResult Allocate()
        {
            return Created(new Uri(""),  "");
        }
    }
}