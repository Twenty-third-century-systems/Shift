using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cabinet.Dtos.External.Request;
using Cabinet.Dtos.External.Response;
using Fridge.Constants;
using Fridge.Contexts;
using Fridge.Models;
using Microsoft.EntityFrameworkCore;
using TurnTable.ExternalServices.Paynow;

namespace TurnTable.ExternalServices.Payments {
    public class PaymentService : IPaymentService {
        private IPayNowService _payNowService;
        private MainDatabaseContext _context;
        private IMapper _mapper;

        public PaymentService(MainDatabaseContext context, IPayNowService payNowService, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _payNowService = payNowService;
        }

        public void TopUp(Guid user, NewPaymentRequestDto dto)
        {
            var transaction = _context.Database.BeginTransaction();
            var paymentTransaction =
                new Transaction(user, (EWalletProviders) dto.WalletProvider, dto.Email, dto.PhoneNumber);
            paymentTransaction.TopUpDescription();
            paymentTransaction.Credit(dto.Amount);
            _context.Transactions.AddAsync(paymentTransaction);
            _context.SaveChangesAsync();

            if (_payNowService.PaymentPlaced(paymentTransaction))
            {
                paymentTransaction.PollUrl = _payNowService.GetPollUrl();
                _context.Update(paymentTransaction);
                _context.SaveChangesAsync();
                transaction.CommitAsync();
            }
            else
            {
                transaction.Rollback();
            }
        }

        public async Task<IEnumerable<TransactionResponseDto>> GetTransactionHistoryAsync(Guid user)
        {
            var transactions =
                await _context.Transactions.Where(t => t.User.CompareTo(user).Equals(0)).ToListAsync();

            var creditTransactions =
                transactions.Where(t =>
                    !t.CreditAmount.Equals(null) && !string.IsNullOrEmpty(t.PayNowReference)).ToList();

            var debitTransactions =
                transactions.Where(t => !t.DebitAmount.Equals(null)).ToList();

            return _mapper.Map<List<TransactionResponseDto>>(
                creditTransactions.Concat(debitTransactions));
        }

        public double GetBalanceAsync(IEnumerable<TransactionResponseDto> transactionHistory)
        {
            double creditAmount = 0;
            double debitAmount = 0;

            foreach (var transaction in transactionHistory)
            {
                if (transaction.CreditAmount > 0)
                    creditAmount += transaction.CreditAmount;
                if (transaction.DebitAmount > 0)
                    debitAmount += transaction.DebitAmount;
            }

            return creditAmount - debitAmount;
        }

        public async Task<bool> CanGetServiceAsync(EService service, Guid user)
        {
            var priceItem = GetPriceAsync(service);
            return GetBalanceAsync(await GetTransactionHistoryAsync(user)) - await GetPriceAsync(service) >= 0;
        }

        private async Task<double> GetPriceAsync(EService service)
        {
            var price = await _context.PriceItems.SingleAsync(p => p.Service.Equals(service));
            return price.Price;
        }

        public async Task<double> GetBalanceAsync(Guid user)
        {
            return GetBalanceAsync(await GetTransactionHistoryAsync(user));
        }

        public async Task BillAsync(EService service, Guid user, string reference)
        {
            var transaction = new Transaction(user);
            transaction.NameSearchPaymentDescription(reference);
            transaction.Debit(await GetPriceAsync(service));
            if (await CanGetServiceAsync(service, user))
                await _context.SaveChangesAsync();
            else
                throw new Exception("Insufficient funds");
        }
    }
}