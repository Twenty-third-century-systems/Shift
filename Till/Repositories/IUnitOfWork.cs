using System;
using System.Threading.Tasks;
using Till.Repositories.Credit;
using Till.Repositories.Payment;
using Till.Repositories.PriceList;

namespace Till.Repositories {
    public interface IUnitOfWork : IDisposable {
        ICreditRepository Credits { get; }
        IPaymentRepository Payments { get; }
        IPriceListRepository PriceList { get; }
        Task<int> CommitAsync();
    }
}