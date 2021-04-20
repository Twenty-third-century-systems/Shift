using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cabinet.Dtos.External.Request;
using Cabinet.Dtos.External.Response;
using Fridge.Constants;
using Fridge.Contexts;
using Fridge.Models.Payments;
using Microsoft.EntityFrameworkCore;
using TurnTable.ExternalServices.Paynow;

namespace TurnTable.ExternalServices.Payments {
    public class PaymentsService : IPaymentsService {
        private readonly IPayNowService _payNowService;
        private readonly PaymentsDatabaseContext _paymentsContext;
        private readonly IMapper _mapper;

        public PaymentsService(PaymentsDatabaseContext paymentsContext, IPayNowService payNowService, IMapper mapper)
        {
            _mapper = mapper;
            _paymentsContext = paymentsContext;
            _payNowService = payNowService;
        }

        public async Task<string> TopUp(Guid user, NewPaymentRequestDto dto)
        {
            var contextTransaction = await _paymentsContext.Database.BeginTransactionAsync();

            var paymentTransaction =
                new Transaction(user, (EWalletProviders) dto.WalletProvider, dto.Email, dto.PhoneNumber);
            paymentTransaction.TopUpDescription();
            paymentTransaction.Credit(dto.Amount);
            await _paymentsContext.Transactions.AddAsync(paymentTransaction);
            await _paymentsContext.SaveChangesAsync();

            if (_payNowService.PaymentPlaced(paymentTransaction))
            {
                paymentTransaction.PollUrl = _payNowService.GetPollUrl();
                _paymentsContext.Update(paymentTransaction);
                await _paymentsContext.SaveChangesAsync();
                await contextTransaction.CommitAsync();
                return _payNowService.GetInstructions();
            }

            await contextTransaction.RollbackAsync();
            return null;
        }

        public async Task<IEnumerable<TransactionResponseDto>> GetTransactionHistoryAsync(Guid user)
        {
            var transactions =
                await _mapper
                    .ProjectTo<TransactionResponseDto>(_paymentsContext.Transactions.Where(t => t.User.Equals(user)))
                    .ToListAsync();

            return transactions.Where(t =>
                t.DebitAmount > 0 || (t.CreditAmount > 0 && !string.IsNullOrEmpty(t.PayNowReference))).ToList();
        }

        public async Task<Guid> BillAsync(EService service, Guid user, string reference)
        {
            var transaction = new Transaction(user);
            transaction.NameSearchPaymentDescription(reference);
            transaction.Debit(await GetPriceAsync(service));
            if (await CanGetServiceAsync(service, user))
            {
                var balance = await _paymentsContext.Balances.SingleAsync(b => b.User.Equals(user));
                var originalBalance = balance.Amount;
                if (transaction.DebitAmount != null)
                {
                    var newBalance = originalBalance - transaction.DebitAmount.Value;
                    balance.UpDateAmount(_paymentsContext, originalBalance, newBalance);
                }

                try
                {
                    await _paymentsContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // TODO LOG instance of concurrency conflict here
                    return await BillAsync(service, user, reference);
                }

                _paymentsContext.Add(transaction);
                await _paymentsContext.SaveChangesAsync();
                return transaction.TransactionId;
            }

            throw new Exception("Insufficient funds");
        }

        public async Task<List<PriceListItemRequestDto>> GetPriceListAsync()
        {
            return await _mapper.ProjectTo<PriceListItemRequestDto>(_paymentsContext.PriceItems).ToListAsync();
        }

        public async Task<bool> CanGetServiceAsync(EService service, Guid user)
        {
            return await GetBalanceAsync(user) - await GetPriceAsync(service) >= 0;
        }

        private async Task<double> GetPriceAsync(EService service)
        {
            var price = await _paymentsContext.PriceItems.SingleOrDefaultAsync(p => p.Service.Equals(service));
            if (price == null)
                throw new Exception("Not a billable service");
            return price.Price;
        }

        public async Task<double> GetBalanceAsync(Guid user)
        {
            var balance = await _paymentsContext.Balances.SingleOrDefaultAsync(b => b.User.Equals(user));
            if (balance == null)
                return 0;
            return balance.Amount;
        }
    }
}