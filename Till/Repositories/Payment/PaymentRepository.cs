using Till.Contexts;
using Till.Repositories.Base;

namespace Till.Repositories.Payment {
    public class PaymentRepository : Repository<Models.Payment>, IPaymentRepository {
        public PaymentRepository(DatabaseContext context) : base(context)
        {
        }
    }
}