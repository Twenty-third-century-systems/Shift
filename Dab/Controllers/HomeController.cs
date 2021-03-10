using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dab.Models;
using Drinkers.ExternalClients.NameSearch;

namespace Dab.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private readonly INameSearchApiClientService _nameSearchApiClientService;

        public HomeController(ILogger<HomeController> logger,INameSearchApiClientService nameSearchApiClientService)
        {
            _logger = logger;
            _nameSearchApiClientService = nameSearchApiClientService;
        }

        public async Task<IActionResult> Index()
        {
            // var nameClaim = User
            //     .Claims
            //     .Where(c => c.Type.Equals("name")&& c.Issuer.Equals("https://localhost:5001"))
            //     .FirstOrDefault();
            //
            // using (var client = new HttpClient())
            // {
            //     var accessToken = await HttpContext.GetTokenAsync("access_token");
            //     client.SetBearerToken(accessToken);
            //
            // var responseMessage = await client.GetAsync(ApiUrls.DashboardValues);
            //     if (responseMessage.IsSuccessStatusCode)
            //     {
            //         ViewBag.DashData = await responseMessage.Content.ReadAsAsync<ExternalUserDashboardDto>();
            //     }
            // }
            // ViewBag.User = nameClaim.Value;
            var nameClaim = User.Claims
                .FirstOrDefault(c => c.Type.Equals("name") && c.Issuer.Equals("https://localhost:5001"));
            ViewBag.DashData = await _nameSearchApiClientService.GetDashBoardDefaultsAsync();
            if (nameClaim != null) ViewBag.User = nameClaim.Value;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}