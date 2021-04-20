using System.Threading.Tasks;
using Cabinet.Dtos.External.Response;

namespace Drinkers.ExternalApiClients.Outputs {
    public interface IOutputsApiService {
        Task<ReservedNameRequestDto> GetNameSearchInfoForDocAsync(int applicationId);
        Task<int> PrivateEntityNameSearchSummaryAsync(string applicationId);
        Task<PrivateEntitySummaryRequestDto> PrivateEntitySummaryAsync(int applicationId);
        Task<RegisteredPrivateEntityRequestDto> RegisteredPrivateEntityAsync(int applicationId);
    }
}