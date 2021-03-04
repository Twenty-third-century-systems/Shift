using System.Collections.Generic;
using System.Threading.Tasks;
using Cabinet.Dtos.External.Request;
using Cabinet.Dtos.External.Response;

namespace Dab.Clients {
    public interface IApiClientService {
        Task<ExternalUserDashboardRequestDto> GetDashBoardDefaultsAsync();
        Task<NameSearchDefaultsRequestDto> GetNameSearchDefaultsAsync();
        Task<SubmittedNameSearchResponseDto> SubmitNewNameSearchAsync(NewNameSearchRequestDto newNameSearchRequestDto);
        Task<bool> IsNameAvailableAsync(string nameToSend);
        Task<List<RegisteredNameResponseDto>> GetApplicableNamesAsync();
    }
}
        