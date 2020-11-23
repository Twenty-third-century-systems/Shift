using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Till.Contexts;
using Till.Repositories.Base;

namespace Till.Repositories.Payment {
    public class PaymentRepository : Repository<Models.Payment>, IPaymentRepository {
        public PaymentRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Models.Payment>> GetUnreconsiledPaymentsByUser(Guid user)
        {
            return await DatabaseContext.Payments
                .Where(p => 
                    p.UserId.ToString().Equals(user.ToString()) 
                    && p.ModeOfPayment != null
                    && p.PaynowRef == null
                     )
                .ToListAsync();
        }

        private DatabaseContext DatabaseContext
        {
            get { return Context as DatabaseContext; }
        }
    }
}