using Till.Contexts;
using Till.Repositories.Base;

namespace Till.Repositories.Credit {
    public class CreditRepository : Repository<Models.Credit>, ICreditRepository {
        public CreditRepository(DatabaseContext context) : base(context)
        {
        }
    }
}