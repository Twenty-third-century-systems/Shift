using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DanceFlow.Controllers {
    [Authorize]
    public class HomeController : Controller {

        public IActionResult Index()
        {
            var nameClaim = User
                .Claims
                .FirstOrDefault(c => c.Type.Equals("full name")&& c.Issuer.Equals("https://localhost:5002"));
            if (nameClaim != null) ViewBag.User = nameClaim.Value;
            return View();
        }
    }
}