using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fridge.Contexts;
using Fridge.Models.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TurnTable.ExternalServices.Paynow;

namespace BarTender.Background {
    public class CheckPaymentStatusService : BackgroundService {
        private readonly IServiceProvider _serviceProvider;

        public CheckPaymentStatusService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Polls the Paynow server for payment statuses
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // TODO: Implement delete for overstayed transaction
                using var scope = _serviceProvider.CreateScope();
                // Getting required services
                var context = scope.ServiceProvider.GetRequiredService<PaymentsDatabaseContext>();
                var payNowService = scope.ServiceProvider.GetRequiredService<IPayNowService>();

                // Getting all unconfirmed payment transactions
                var transactions =
                    await context.Transactions
                        .Where(t => t.PayNowReference == null && t.PollUrl != null)
                        .ToListAsync(cancellationToken: stoppingToken);

                // Confirming all unconfirmed transactions
                try
                {
                    foreach (var transaction in transactions)
                    {
                        if (payNowService.WasPaid(transaction.PollUrl))
                        {
                            transaction.PayNowReference = payNowService.GetPayNowReference();
                            var balance =
                                await context.Balances.SingleOrDefaultAsync(b => b.User.Equals(transaction.User),
                                    stoppingToken);
                            if (balance == null)
                                balance = new Balance
                                {
                                    User = transaction.User
                                };
                            balance.Amount += transaction.CreditAmount.Value;
                            context.Update(balance);

                            await context.SaveChangesAsync(stoppingToken);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    // throw;
                    //TODO LOG ERROR HERE
                }
            }
        }
    }
}