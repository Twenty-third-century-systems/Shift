using Fridge.Models;
using Fridge.Models.Payments;

namespace TurnTable.ExternalServices.Paynow {
    public interface IPayNowService {
        bool PaymentPlaced(Transaction transaction);
        string GetPollUrl();
        bool WasPaid(string pollUrl);
        string GetPayNowReference();
        public string GetInstructions();
    }
}