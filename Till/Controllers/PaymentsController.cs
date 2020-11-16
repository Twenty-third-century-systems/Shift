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

        public PaymentsController(IMapper mapper, ICounterService counterService)
        {
            _counterService = counterService;
            _mapper = mapper;
        }

        [HttpPost("Topup")]
        public async Task<IActionResult> Topup([FromForm] PaymentDataDto data)
        {
            var payment = _mapper.Map<PaymentData, Payment>(data.PaymentData);

            payment.UserId = Guid.Parse("0cf502e8-3c92-48f6-ab59-9421efb532dc");
            payment.PaymentId = Guid.NewGuid();
            payment.Date = DateTime.Now;
            payment.Description = "Top up";

            var addPayment = await _counterService.AddPayment(payment);

            var paynow = new Paynow("9945", "1a42766b-1fea-48f6-ac39-1484dddfeb62");
            paynow.ResultUrl = "https://localhost:44313/Payments/Result";
            paynow.ReturnUrl = "https://localhost:44313";

            var paynowPayment = paynow.CreatePayment(payment.PaymentId.ToString(), payment.Email);
            paynowPayment.Add("Top up", 1);

            string instr = "";

            var response = paynow.SendMobile(paynowPayment, data.PaymentData.PNumber, "ecocash");
            if (response.Success())
            {
                // Get the url to redirect the user to so they can make payment
                var link = response.RedirectLink();

                // Get the poll url (used to check the status of a transaction). You might want to save this in your DB
                var pollUrl = response.PollUrl();
                // await checkTransactionAsync(paynow, pollUrl);

                // Get the instructions
                instr = response.Instructions();
                return Ok(
                    "Please attend to the prompt displayed on your device.\nIf nothing is displayed kindly dial\n");
            }
            else
            {
                return StatusCode(StatusCodes.Status502BadGateway,"Something happened while trying to process topup");
            }
        }
    }
}