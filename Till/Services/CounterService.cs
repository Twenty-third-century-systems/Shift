using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Till.Dtos;
using Till.Models;
using Till.Repositories;

namespace Till.Services {
    public class CounterService : ICounterService {
        private IUnitOfWork _unitOfWork;
        private IPaynowService _paynowService;
        private IMapper _mapper;

        public CounterService(IUnitOfWork unitOfWork, IPaynowService paynowService, IMapper mapper)
        {
            _mapper = mapper;
            _paynowService = paynowService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Payment> AddTopupAsync(Payment payment, TopUpDataDto data, string value)
        {
            payment.UserId = Guid.Parse(value);
            payment.PaymentId = Guid.NewGuid();
            payment.Email = data.TopupData.Email;
            payment.Date = DateTime.Now;
            payment.ModeOfPayment = data.TopupData.Mode;
            payment.Description = "Top up";
            payment.CreditAmount = data.TopupData.Amount;

            await _unitOfWork.Payments.AddAsync(payment);
            await _unitOfWork.CommitAsync();
            return payment;
        }

        public async Task<Payment> UpdateTopupAsync(Payment paymentToBeUpdated, Payment paymentToUpdate)
        {
            paymentToBeUpdated.PollUrl = paymentToUpdate.PollUrl;
            await _unitOfWork.CommitAsync();
            return paymentToBeUpdated;
        }

        public async Task DeletePaymentAsync(Payment addedPayment)
        {
            _unitOfWork.Payments.Remove(addedPayment);
            await _unitOfWork.CommitAsync();
        }

        public async Task ReconcileAccountsAsync(Guid user)
        {
            IEnumerable<Payment> paymentsByUser = new List<Payment>();
            bool recheck = false;
            paymentsByUser = await _unitOfWork.Payments.GetUnreconsiledPaymentsByUser(user);
            foreach (var payment in paymentsByUser)
            {
                if (payment.PaynowRef == null && payment.ModeOfPayment != null && payment.PollUrl != null)
                {
                    recheck = await CheckPaymentAsync(payment);
                }
            }

            if (recheck)
                await ReconcileAccountsAsync(user);
        }

        public async Task<AccountPaymentsDto> GetAccountHistAndBalanceAsync(Guid user)
        {
            var account = new AccountPaymentsDto();
            account.Transactions =
                _unitOfWork.Payments.Find(p =>
                    p.UserId.ToString().Equals(user.ToString())
                    && p.Success
                ).ToList();
            account.Balance = await CalculateBalanceAsync(account.Transactions);
            return account;
        }

        public async Task<Payment> AddPaymentAsync(PaymentDataDto paymentDataDto)
        {
            var payment = _mapper.Map<PaymentDataDto, Payment>(paymentDataDto);
            // payment.UserId = pau;
            payment.PaymentId = Guid.NewGuid();
            payment.Date = DateTime.Now;
            var byIdAsync = await _unitOfWork.PriceList.GetByIdAsync(paymentDataDto.Service);
            payment.Description = byIdAsync.Service;
            if (await CanPayAsync(payment.UserId, byIdAsync.Price))
            {
                payment.DebitAmount = byIdAsync.Price;
                payment.Success = true;
                await _unitOfWork.Payments.AddAsync(payment);
                await _unitOfWork.CommitAsync();
                return (payment);
            }
            return (null);
        }

        public async Task<IEnumerable<PriceListItemDto>> GetPricesAsync()
        {
            var allAsync = await _unitOfWork.PriceList.GetAllAsync();
            return _mapper.Map<IEnumerable<PriceList>, IEnumerable<PriceListItemDto>>(allAsync);
        }

        private async Task<bool> CanPayAsync(Guid paymentUserId, double amount)
        {
            var accountHistAndBalanceAsync = await GetAccountHistAndBalanceAsync(paymentUserId);
            return accountHistAndBalanceAsync.Balance - amount >= 0;
        }

        private async Task<double> CalculateBalanceAsync(IEnumerable<Payment> paymentsByUser)
        {
            double credit = 0;
            double debit = 0;
            foreach (var payment in paymentsByUser)
            {
                if (payment.CreditAmount != null)
                    credit = credit + payment.CreditAmount.Value;

                if (payment.DebitAmount != null)
                    debit = debit + payment.DebitAmount.Value;
            }

            return credit - debit;
        }

        private async Task<Boolean> CheckPaymentAsync(Payment payment)
        {
            bool refresh = false;
            var paynow = await _paynowService.GetPaynow();
            var statusResponse = paynow.PollTransaction(payment.PollUrl);

            if (statusResponse.Paid())
            {
                var paymentInformation = statusResponse.GetData();
                if (paymentInformation.ContainsKey("paynowreference"))
                {
                    lock (_unitOfWork.Payments)
                    {
                        var byIdAsync = _unitOfWork.Payments.GetByIdAsync(payment.Id).Result;
                        if (byIdAsync.PaynowRef == null)
                            payment.PaynowRef = paymentInformation["paynowreference"];
                    }
                }

                payment.Success = true;
                await _unitOfWork.CommitAsync();
                refresh = true;
            }

            return refresh;
        }
    }
}