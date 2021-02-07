using System;
using System.Threading.Tasks;
using Cabinet.Dtos;
using Cabinet.Dtos.Request;
using Cabinet.Dtos.Response;

namespace TurnTable.ExternalServices {
    public interface INameSearchService {
        public Task<bool> NameAvailable(string suggestedName);

        public Task<SubmittedNameSearchResponseDto> CreateNewNameSearch(Guid userId, NewNameSearchRequestDto dto);
    }
}