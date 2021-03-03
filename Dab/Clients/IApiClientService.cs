using System.Threading.Tasks;
using Cabinet.Dtos.External.Request;
using Cabinet.Dtos.External.Response;

namespace Dab.Clients {
    public interface IApiClientService {
        Task<ExternalUserDashboardRequestDto> GetDashBoardDefaultsAsync();
        Task<NameSearchDefaultsRequestDto> GetNameSearchDefaultsAsync();
        Task<SubmittedNameSearchResponseDto> PostNewNameSearchAsync(NewNameSearchRequestDto newNameSearchRequestDto);
        Task<bool> GetNameAvailabilityAsync(string nameToSend);
    }
}