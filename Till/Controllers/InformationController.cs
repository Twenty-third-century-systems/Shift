using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TurnTable.ExternalServices.Payments;

namespace Till.Controllers {
    [Authorize]
    [Route("information")]
    public class InformationController : Controller {
        private readonly IPaymentsService _paymentsService;

        public InformationController(IPaymentsService paymentsService)
        {
            _paymentsService = paymentsService;
        }

        [HttpGet("PriceList")]
        public async Task<IActionResult> PriceList()
        {
            ViewBag.PriceList = await _paymentsService.GetPriceListAsync();
            return View();
        }
    }
}