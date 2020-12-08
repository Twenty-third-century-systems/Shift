using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Till.Models;
using Till.Services;

namespace Till.Controllers {
    [Authorize]
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private ICounterService _counterService;

        public HomeController(ILogger<HomeController> logger,ICounterService counterService)
        {
            _counterService = counterService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var userIdClaim = User
                .Claims
                .FirstOrDefault(c => c.Type.Equals("sub")&& c.Issuer.Equals("https://localhost:5001"));

            if (userIdClaim == null)
                return Unauthorized();
            
            await _counterService.ReconcileAccountsAsync(Guid.Parse(userIdClaim.Value));
            var accountHistAndBalanceAsync = 
                await _counterService.GetAccountHistAndBalanceAsync(Guid.Parse(userIdClaim.Value));
            accountHistAndBalanceAsync.Transactions =
                accountHistAndBalanceAsync.Transactions.OrderByDescending(p => p.Date)
                    .ToList();
            
            var nameClaim = User
                .Claims
                .FirstOrDefault(c => c.Type.Equals("name")&& c.Issuer.Equals("https://localhost:5001"));
            ViewBag.User = nameClaim.Value;
            ViewBag.ReconsiledAcount = accountHistAndBalanceAsync;
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