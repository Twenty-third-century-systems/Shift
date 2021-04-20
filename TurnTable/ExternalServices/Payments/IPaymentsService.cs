using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cabinet.Dtos.External.Request;
using Cabinet.Dtos.External.Response;
using Fridge.Constants;

namespace TurnTable.ExternalServices.Payments {
    public interface IPaymentsService {
        Task<string> TopUp(Guid user, NewPaymentRequestDto dto);
        Task<IEnumerable<TransactionResponseDto>> GetTransactionHistoryAsync(Guid user);
        Task<bool> CanGetServiceAsync(EService service, Guid user);
        Task<double> GetBalanceAsync(Guid user);
        Task<Guid> BillAsync(EService service, Guid user, string reference);
        Task<List<PriceListItemRequestDto>> GetPriceListAsync();
    }
}