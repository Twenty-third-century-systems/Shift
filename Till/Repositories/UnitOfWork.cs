using System.Threading.Tasks;
using Till.Contexts;
using Till.Repositories.Credit;
using Till.Repositories.Payment;
using Till.Repositories.PriceList;

namespace Till.Repositories {
    public class UnitOfWork:IUnitOfWork {
        private DatabaseContext _context;
        private CreditRepository _creditRepository;
        private PaymentRepository _paymentRepository;
        private PriceListRepository _priceListRepository;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }

        public ICreditRepository Credits => _creditRepository ?? new CreditRepository(_context);
        public IPaymentRepository Payments => _paymentRepository ?? new PaymentRepository(_context);
        public IPriceListRepository PriceList => _priceListRepository ?? new PriceListRepository(_context);
        
        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
        
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}