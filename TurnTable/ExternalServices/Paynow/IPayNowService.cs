using Fridge.Models;

namespace TurnTable.ExternalServices.Paynow {
    public interface IPayNowService {
        bool PaymentPlaced(Transaction transaction);
        string GetPollUrl();
        bool WasPaid(string pollUrl);
        string GetPayNowReference();
    }
}