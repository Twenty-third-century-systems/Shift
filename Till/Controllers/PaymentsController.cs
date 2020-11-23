using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Till.Dtos;
using Till.Services;
using Webdev.Payments;
using Payment = Till.Models.Payment;

namespace Till.Controllers {
    [ApiController]
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
            var addedPayment = await _counterService.AddTopup(payment, data);

            // Initialising Paynow
            var paynow = await _paynowService.GetPaynow();
            var paynowPayment = paynow.CreatePayment(payment.PaymentId.ToString(), payment.Email);
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
                await _counterService.DeletePayment(addedPayment);
                return StatusCode(StatusCodes.Status502BadGateway, "Something happened while trying to process top up");
            }
        }

        [HttpPost("Service/Pay")]
        public async Task<ActionResult> Payment(PaymentDataDto paymentDataDto)
        {
            var addPayment = await _counterService.AddPayment(paymentDataDto);
            if (addPayment != null)
                return Created(String.Empty, addPayment);
            return BadRequest("Insufficient funds");
        }
    }
}