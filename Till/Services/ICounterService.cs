using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cabinet.Dtos.External.Request;
using Cabinet.Dtos.External.Response;
using Till.Dtos;
using Till.Models;

namespace Till.Services {
    public interface ICounterService {
        Task<Payment> AddTopupAsync(Payment payment, TopUpDataDto data, string value);
        Task<Payment> UpdateTopupAsync(Payment paymentToBeUpdated, Payment paymentToUpdate);
        Task DeletePaymentAsync(Payment addedPayment);
        Task ReconcileAccountsAsync(Guid user);
        Task<AccountPaymentsDto> GetAccountHistAndBalanceAsync(Guid user);
        Task<Payment> AddPaymentAsync(PaymentDataDto paymentDataDto);
        Task<IEnumerable<PriceListItemRequestDto>> GetPricesAsync();
    }
}