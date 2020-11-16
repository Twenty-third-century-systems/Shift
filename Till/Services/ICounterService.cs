using System.Threading.Tasks;
using Till.Models;

namespace Till.Services {
    public interface ICounterService {
        Task<Payment> AddPayment(Payment payment);
    }
}