using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Till.Dtos;
using Till.Services;
using Webdev.Payments;
using Payment = Till.Models.Payment;

namespace Till.Controllers {
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class PaymentsController : Controller {
        private IMapper _mapper;
        private ICounterService _counterService;
        private IPaynowService _paynowService;

        public PaymentsController(IMapper mapper, ICounterService counterService, IPaynowService paynowService)
        {
            _paynowService = paynowService;
            _counterService = counterService;
            _mapper = mapper;
        }

        [HttpPost("Topup")]
        public async Task<IActionResult> Topup([FromForm] TopUpDataDto data)
        {
            //Initialising Payment record
            var payment = _mapper.Map<TopupData, Payment>(data.TopupData);

            // Saving to db
            var userIdClaim = User
                .Claims
                .FirstOrDefault(c => c.Type.Equals("sub") && c.Issuer.Equals("https://localhost:5001"));
            var addedPayment = await _counterService.AddTopupAsync(payment, data, userIdClaim.Value);

            // Initialising Paynow
            var paynow = await _paynowService.GetPaynow();
            var paynowPayment =
                paynow.CreatePayment(payment.PaymentId.ToString(), payment.Email);
            paynowPayment.Add("Top up", (float) data.TopupData.Amount);

            var response = paynow.SendMobile(paynowPayment, data.TopupData.PNumber, data.TopupData.Mode);
            if (response.Success())
            {
                // Get the poll url (used to check the status of a transaction). You might want to save this in your DB
                payment.PollUrl = response.PollUrl();
                // await checkTransactionAsync(paynow, pollUrl);
                var updatePaymentAsync = await _counterService.UpdateTopupAsync(addedPayment, payment);
                if (updatePaymentAsync != null)
                    return Created(String.Empty, response.Instructions());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            else
            {
                await _counterService.DeletePaymentAsync(addedPayment);
                return StatusCode(StatusCodes.Status502BadGateway, "Something happened while trying to process top up");
            }
        }

        [AllowAnonymous]
        [HttpPost("Service")]
        public async Task<ActionResult> Payment(PaymentDataDto paymentDataDto)
        {
            var addPayment = await _counterService.AddPaymentAsync(paymentDataDto);
            if (addPayment != null)
                return Created(String.Empty, addPayment);
            return BadRequest("Insufficient funds");
        }
    }
}