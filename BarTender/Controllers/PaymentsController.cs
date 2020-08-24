using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarTender.Controllers
{
    [Route("payments")]
    public class PaymentsController : Controller
    {
        [HttpGet("claims")]
        public IActionResult Get()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}