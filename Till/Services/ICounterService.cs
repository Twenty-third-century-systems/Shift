using System;
using System.Threading.Tasks;
using Till.Dtos;
using Till.Models;

namespace Till.Services {
    public interface ICounterService {
        Task<Payment> AddTopup(Payment payment, TopUpDataDto data);
        Task<Payment> UpdateTopupAsync(Payment paymentToBeUpdated, Payment paymentToUpdate);
        Task DeletePayment(Payment addedPayment);
        Task ReconcileAccountsAsync(Guid user);
        Task<AccountPaymentsDto> GetAccountHistAndBalanceAsync(Guid user);
        Task<Payment> AddPayment(PaymentDataDto paymentDataDto);
    }
}