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
            await _counterService.ReconcileAccountsAsync(Guid.Parse("0cf502e8-3c92-48f6-ab59-9421efb532dc"));
            var accountHistAndBalanceAsync = 
                await _counterService.GetAccountHistAndBalanceAsync(Guid.Parse("0cf502e8-3c92-48f6-ab59-9421efb532dc"));
            accountHistAndBalanceAsync.Transactions =
                accountHistAndBalanceAsync.Transactions.OrderByDescending(p => p.Date)
                    .ToList();
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