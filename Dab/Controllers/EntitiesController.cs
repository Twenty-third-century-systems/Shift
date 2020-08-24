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
    }
}