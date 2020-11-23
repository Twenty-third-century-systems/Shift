using System.Threading.Tasks;
using Webdev.Payments;

namespace Till.Services {
    public interface IPaynowService {
        Task<Paynow> GetPaynow();
    }
}