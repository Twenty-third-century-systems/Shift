using System;
using System.Threading.Tasks;
using Cabinet.Dtos.External.Request;
using Cabinet.Dtos.External.Response;

namespace TurnTable.ExternalServices.NameSearch {
    public interface INameSearchService {
        public bool NameIsAvailable(string suggestedName);

        public Task<SubmittedNameSearchResponseDto> CreateNewNameSearchAsync(Guid user, NewNameSearchRequestDto dto);
        Task<int> TestApproveNameSearch(Guid user, int applicationId);

        public Task<int> FurtherReserveUnexpiredNameAsync(string reference);
    }
}