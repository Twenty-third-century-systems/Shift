using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dab.Controllers {
    [Route("entity")]
    public class EntitiesController : Controller {
        // GET
        [HttpGet("{nameId}/new")]
        public IActionResult NewPrivateEntity(string nameId)
        {
            return View();
        }

        [HttpGet("names/reg")]
        public IActionResult RegisteredNames()
        {
            var userIdClaim = User.Claims.Where(c => c.Type.Equals("sub") && c.Issuer.Equals("https://localhost:5001"))
                .FirstOrDefault();
            ViewBag.User = userIdClaim.Value;
            return Ok();
        }
    }
}