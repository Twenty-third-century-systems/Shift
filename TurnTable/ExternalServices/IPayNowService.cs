using System;
using Fridge.Models;

public interface IPayNowService {
    bool PaymentPlaced(Transaction transaction);
    string GetPollUrl();
    bool WasPaid(string pollUrl);
    string GetPayNowReference();
}