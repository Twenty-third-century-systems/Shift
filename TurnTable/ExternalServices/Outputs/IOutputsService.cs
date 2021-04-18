using System.Threading.Tasks;
using Cabinet.Dtos.External.Response;

namespace TurnTable.ExternalServices.Outputs {
    public interface IOutputsService {
        Task<ReservedNameRequestDto> NameSearchSummary(int applicationId);
        Task<int> GetUsedNameSearchApplicationId(int applicationId);
        Task<PrivateEntitySummaryRequestDto> GetRegisteredPrivateEntitySummary(int applicationId);
        Task<RegisteredPrivateEntityRequestDto> GetRegisteredPrivateEntity(int applicationId);
    }
}