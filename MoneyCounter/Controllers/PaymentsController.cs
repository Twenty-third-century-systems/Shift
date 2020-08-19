using Microsoft.AspNetCore.Mvc;
using MoneyCounter.Dtos;

namespace MoneyCounter.Controllers {
    [Route("api/payments")]
    public class PaymentsController : Controller {
        // post
        [HttpPost("payment")]
        public IActionResult NewPayment([FromBody] PaymentFromClientDto payment)
        {
            return Ok();
        }
    }
}