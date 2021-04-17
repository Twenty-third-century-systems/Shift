using System.Threading.Tasks;
using Cabinet.Dtos.Internal.Request;

namespace Drinkers.InternalClients.PvtEntity {
    public interface IPvtEntityApiClientService {        
        Task<bool> Query(RaisedQueryRequestDto dto);
        Task<bool> FinishAsync(int applicationId);
    }
}