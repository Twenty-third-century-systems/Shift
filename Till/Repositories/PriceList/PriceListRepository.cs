using Till.Contexts;
using Till.Repositories.Base;

namespace Till.Repositories.PriceList {
    public class PriceListRepository : Repository<Models.PriceList>, IPriceListRepository {
        public PriceListRepository(DatabaseContext context) : base(context)
        {
        }
    }
}