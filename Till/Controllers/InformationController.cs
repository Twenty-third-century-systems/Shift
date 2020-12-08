using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Till.Services;

namespace Till.Controllers {
    [Route("[controller]")]
    public class InformationController : Controller {
        private ICounterService _counterService;

        public InformationController(ICounterService counterService)
        {
            _counterService = counterService;
        }

        [HttpGet("PriceList")]
        public async Task<IActionResult> PriceList()
        {
            var priceListItemDtos = await _counterService.GetPricesAsync();
            ViewBag.PriceList = priceListItemDtos.ToList();
            return View();
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAccountInformation(Guid userId)
        {
            var accountHistAndBalanceAsync =
                await _counterService.GetAccountHistAndBalanceAsync(userId);
            return Ok(accountHistAndBalanceAsync.Balance);
        }
    }
}