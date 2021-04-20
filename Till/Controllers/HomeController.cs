using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Till.Models;
using TurnTable.ExternalServices.Payments;

namespace Till.Controllers {
    [Authorize]
    public class HomeController : Controller {
        private readonly IPaymentsService _paymentsService;

        public HomeController(IPaymentsService paymentsService)
        {
            _paymentsService = paymentsService;
        }

        public async Task<IActionResult> Index()
        {
            var userIdClaim = User
                .Claims
                .FirstOrDefault(c => c.Type.Equals("sub") && c.Issuer.Equals("https://localhost:5001"));

            var nameClaim = User
                .Claims
                .FirstOrDefault(c => c.Type.Equals("name") && c.Issuer.Equals("https://localhost:5001"));

            if (userIdClaim == null && nameClaim != null)
                return Unauthorized();

            var user = Guid.Parse(userIdClaim.Value);
            var history =
                await _paymentsService.GetTransactionHistoryAsync(user);
            history =
                history.OrderByDescending(t => t.Date)
                    .ToList();

            ViewBag.User = nameClaim.Value;
            ViewBag.AccountHistory = history;
            ViewBag.AccountBalance =
                await _paymentsService.GetBalanceAsync(user);
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