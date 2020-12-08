using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Dab.Dtos;
using Dab.Globals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dab.Models;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace Dab.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var nameClaim = User
                .Claims
                .Where(c => c.Type.Equals("name")&& c.Issuer.Equals("https://localhost:5001"))
                .FirstOrDefault();

            using (var client = new HttpClient())
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                client.SetBearerToken(accessToken);

                var responseMessage = await client.GetAsync(ApiUrls.DashboardValues);
                if (responseMessage.IsSuccessStatusCode)
                {
                    ViewBag.DashData = await responseMessage.Content.ReadAsAsync<ExternalUserDashboardDto>();
                }
            }
            ViewBag.User = nameClaim.Value;
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