using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fridge.Contexts;
using LinqToDB;
using LinqToDB.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BarTender.Background {
    public class CheckPaymentStatusService : BackgroundService {
        private IServiceProvider _serviceProvider;

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
                using (var scope = _serviceProvider.CreateScope())
                {
                    // Getting required services
                    var context = scope.ServiceProvider.GetRequiredService<MainDatabaseContext>();
                    var payNowService = scope.ServiceProvider.GetRequiredService<IPayNowService>();

                    // Getting all unconfirmed payment transactions
                    var transactions =
                        await context.Transactions
                            .Where(t => t.PayNowReference.IsNullOrEmpty() && !t.PollUrl.IsNullOrEmpty())
                            .ToListAsync();
                    
                    // Confirming all unconfirmed transactions
                    foreach (var transaction in transactions)
                    {
                        if (payNowService.WasPaid(transaction.PollUrl))
                        {
                            transaction.PayNowReference = payNowService.GetPayNowReference();
                            context.Update(transaction);
                            await context.SaveChangesAsync();
                        }
                    }
                }
            }
        }
    }
}