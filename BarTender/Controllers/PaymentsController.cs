using Microsoft.AspNetCore.Mvc;

namespace BarTender.Controllers
{
    public class PaymentsController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return Ok();
        }
    }
}