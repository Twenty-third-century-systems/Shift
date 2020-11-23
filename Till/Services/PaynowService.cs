using System.Threading.Tasks;
using Webdev.Payments;

namespace Till.Services {
    public class PaynowService :IPaynowService{
        private Paynow _paynow;

        public PaynowService()
        {
            _paynow = new Paynow("9945", "1a42766b-1fea-48f6-ac39-1484dddfeb62");
            _paynow.ResultUrl = "https://localhost:44313/Payments/Result";
            _paynow.ReturnUrl = "https://localhost:44313";
        }


        public async Task<Paynow> GetPaynow()
        {
            return _paynow;
        }
    }
}