using System;
using System.Linq;
using System.Threading.Tasks;
using Cabinet.Dtos.External.Request;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Till.Dtos;
using TurnTable.ExternalServices.Payments;
using Webdev.Payments;
using Payment = Till.Models.Payment;

namespace Till.Controllers {
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class PaymentsController : Controller {
        private readonly IPaymentsService _paymentsService;

        public PaymentsController(IPaymentsService paymentsService)
        {
            _paymentsService = paymentsService;
        }

        [HttpPost("Topup")]
        public async Task<IActionResult> Topup([FromForm] NewPaymentRequestDto dto)
        {
            try
            {
                var instructions =
                    await _paymentsService.TopUp(Guid.Parse("375cad3c-ed53-4757-a186-02d15cfcc110"), dto);
                if (!instructions.IsNullOrEmpty())
                {
                    return Created("", instructions);
                } 

                return BadRequest();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }

            // //Initialising Payment record
            // var payment = _mapper.Map<TopupData, Payment>(data.TopupData);
            //
            // // Saving to db
            // var userIdClaim = User
            //     .Claims
            //     .FirstOrDefault(c => c.Type.Equals("sub") && c.Issuer.Equals("https://localhost:5001"));
            // var addedPayment = await _counterService.AddTopupAsync(payment, data, userIdClaim.Value);
            //
            // // Initialising Paynow
            // var paynow = await _paynowService.GetPaynow();
            // var paynowPayment =
            //     paynow.CreatePayment(payment.PaymentId.ToString(), payment.Email);
            // paynowPayment.Add("Top up", (float) data.TopupData.Amount);
            //
            // var response = paynow.SendMobile(paynowPayment, data.TopupData.PNumber, data.TopupData.Mode);
            // if (response.Success())
            // {
            //     // Get the poll url (used to check the status of a transaction). You might want to save this in your DB
            //     payment.PollUrl = response.PollUrl();
            //     // await checkTransactionAsync(paynow, pollUrl);
            //     var updatePaymentAsync = await _counterService.UpdateTopupAsync(addedPayment, payment);
            //     if (updatePaymentAsync != null)
            //         return Created(String.Empty, response.Instructions());
            //     return StatusCode(StatusCodes.Status500InternalServerError);
            // }
            // else
            // {
            //     await _counterService.DeletePaymentAsync(addedPayment);
            //     return StatusCode(StatusCodes.Status502BadGateway, "Something happened while trying to process top up");
            // }
        }

        [AllowAnonymous]
        [HttpPost("Service")]
        public async Task<ActionResult> Payment(PaymentDataDto paymentDataDto)
        {
            // var addPayment = await _counterService.AddPaymentAsync(paymentDataDto);
            // if (addPayment != null)
            //     return Created(String.Empty, addPayment);
            return BadRequest("Insufficient funds");
        }
    }
}