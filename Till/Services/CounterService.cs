using System.Threading.Tasks;
using Till.Models;
using Till.Repositories;

namespace Till.Services {
    public class CounterService : ICounterService {
        private IUnitOfWork _unitOfWork;

        public CounterService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Payment> AddPayment(Payment payment)
        {
            await _unitOfWork.Payments.AddAsync(payment);
            await _unitOfWork.CommitAsync();
            return payment;
        }
    }
}