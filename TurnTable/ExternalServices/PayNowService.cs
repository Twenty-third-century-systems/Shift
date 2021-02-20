using System;
using Fridge.Constants;
using Fridge.Models;
using Webdev.Core;
using Webdev.Payments;

namespace TurnTable.ExternalServices {

    public class PayNowService : IPayNowService {
        private Paynow _paynow;
        private InitResponse _paymentResponse;
        private StatusResponse _statusResponse;

        public PayNowService()
        {
            _paynow = new Paynow("9945", "1a42766b-1fea-48f6-ac39-1484dddfeb62");
            _paynow.ResultUrl = "https://localhost:44313/Payments/Result";
            _paynow.ReturnUrl = "https://localhost:44313";
        }

        public bool PaymentPlaced(Transaction transaction)
        {
            if (!_paynow.Equals(null))
            {
                var payment = _paynow.CreatePayment(transaction.TransactionId.ToString(), transaction.Email);
                payment.Add(transaction.Description, transaction.GetAmount());
                _paymentResponse = _paynow.SendMobile(payment, transaction.PhoneNumber, EWalletProviders.Ecocash.ToString());
                return _paymentResponse.Success();
            }

            return false;
        }

        public string GetPollUrl()
        {
            if (!_paymentResponse.Equals(null))
                return _paymentResponse.PollUrl();
            return null;
        }

        public bool WasPaid(string pollUrl)
        {
            _statusResponse = _paynow.PollTransaction(pollUrl);
            return _statusResponse.Paid();
        }

        public string GetPayNowReference()
        {
            if (!_statusResponse.Equals(null))
            {
                var paymentInformation = _statusResponse.GetData();
                if (paymentInformation.ContainsKey("paynowreference"))
                {
                    return paymentInformation["paynowreference"];
                }
            }

            return null;
        }
    }
}