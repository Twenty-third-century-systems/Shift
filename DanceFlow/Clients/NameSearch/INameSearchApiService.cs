using System.Collections.Generic;
using System.Threading.Tasks;
using Cabinet.Dtos.Internal.Request;

namespace DanceFlow.Clients.NameSearch {
    public interface INameSearchApiService {
        Task<bool> ChangeNameStatusAsync(int nameId, int status);
        Task<bool> FinishNameSearchExaminationAsync(int nameSearchId);
        Task<List<NameRequestDto>> GetNamesThatContainAsync(string suggestedName);
        Task<List<NameRequestDto>> GetNamesThatStartWithAsync(string suggestedName);
        Task<List<NameRequestDto>> GetNamesThatEndWithAsync(string suggestedName);
    }
}