using System.Threading.Tasks;
using Cabinet.Dtos.External.Response;

namespace Drinkers.ExternalClients.Outputs {
    public interface IOutputsApiService {
        Task<ReservedNameRequestDto> GetNameSearchInfoForDoc(int applicationId);
        Task<int> PrivateEntityNameSearchSummary(string applicationId);
        Task<PrivateEntitySummaryRequestDto> PrivateEntitySummary(int applicationId);
        Task<RegisteredPrivateEntityRequestDto> RegisteredPrivateEntity(int applicationId);
    }
}