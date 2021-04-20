using System.Linq;
using System.Threading.Tasks;
using Drinkers.ExternalApiClients.NameSearch;
using Microsoft.AspNetCore.Mvc;

namespace Dab.Controllers {
    public class HomeController : Controller {
        private readonly INameSearchApiClientService _nameSearchApiClientService;

        public HomeController(INameSearchApiClientService nameSearchApiClientService)
        {
            _nameSearchApiClientService = nameSearchApiClientService;
        }

        public async Task<IActionResult> Index()
        {
            var nameClaim = User.Claims
                .FirstOrDefault(c => c.Type.Equals("name") && c.Issuer.Equals("https://localhost:5001"));
            ViewBag.DashData = await _nameSearchApiClientService.GetDashBoardDefaultsAsync();
            if (nameClaim != null) ViewBag.User = nameClaim.Value;
            return View();
        }
    }
}