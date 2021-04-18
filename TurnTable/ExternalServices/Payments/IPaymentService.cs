using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cabinet.Dtos.External.Request;
using Cabinet.Dtos.External.Response;
using Fridge.Constants;

namespace TurnTable.ExternalServices.Payments {
    public interface IPaymentService {
        void TopUp(Guid user, NewPaymentRequestDto dto);
        Task<IEnumerable<TransactionResponseDto>> GetTransactionHistoryAsync(Guid user);
        double GetBalanceAsync(IEnumerable<TransactionResponseDto> transactionHistory);
        Task<bool> CanGetServiceAsync(EService service, Guid user);
        Task<double> GetBalanceAsync(Guid user);
        Task BillAsync (EService nameSearch, Guid user, string reference);
    }
}