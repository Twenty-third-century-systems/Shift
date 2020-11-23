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

        public async Task<Payment> AddTopup(Payment payment, TopUpDataDto data)
        {
            payment.UserId = Guid.Parse("0cf502e8-3c92-48f6-ab59-9421efb532dc");
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

        public async Task DeletePayment(Payment addedPayment)
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
                    recheck = await CheckPayment(payment);
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
            account.Balance = await CalculateBalance(account.Transactions);
            return account;
        }

        public async Task<Payment> AddPayment(PaymentDataDto paymentDataDto)
        {
            var payment = _mapper.Map<PaymentDataDto, Payment>(paymentDataDto);
            payment.UserId = Guid.Parse("0cf502e8-3c92-48f6-ab59-9421efb532dc");
            payment.PaymentId = Guid.NewGuid();
            payment.Date = DateTime.Now;
            if (await CanPay(payment.UserId, paymentDataDto.Amount))
            {
                payment.Success = true;
                await _unitOfWork.Payments.AddAsync(payment);
                await _unitOfWork.CommitAsync();
                return (payment);
            }
            return (null);
        }

        private async Task<bool> CanPay(Guid paymentUserId, double amount)
        {
            var accountHistAndBalanceAsync = await GetAccountHistAndBalanceAsync(paymentUserId);
            return accountHistAndBalanceAsync.Balance - amount >= 0;
        }


        private async Task<double> CalculateBalance(IEnumerable<Payment> paymentsByUser)
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

        private async Task<Boolean> CheckPayment(Payment payment)
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