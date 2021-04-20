using System.Threading.Tasks;
using Cabinet.Dtos.Internal.Request;

namespace Drinkers.InternalApiClients.PvtEntity {
    public interface IPvtEntityApiClientService {        
        Task<bool> RaiseQueryAsync(RaisedQueryRequestDto dto);
        Task<bool> FinishAsync(int applicationId);
    }
}