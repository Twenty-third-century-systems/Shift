using System.Collections.Generic;
using System.Threading.Tasks;
using Cabinet.Dtos.External.Request;
using Cabinet.Dtos.External.Response;

namespace Dab.Clients.NameSearch {
    public interface INameSearchApiClientService {
        Task<ExternalUserDashboardRequestDto> GetDashBoardDefaultsAsync();
        Task<NameSearchDefaultsRequestDto> GetNameSearchDefaultsAsync();
        Task<SubmittedNameSearchResponseDto> NewNameSearchAsync(NewNameSearchRequestDto newNameSearchRequestDto);
        Task<bool> IsNameAvailableAsync(string nameToSend);
        Task<List<RegisteredNameResponseDto>> GetApplicableNamesAsync();
        Task<bool> FurtherReserveUnexpiredNameAsync(string reference);
    }
}
        