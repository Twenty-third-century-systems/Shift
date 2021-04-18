using System.Collections.Generic;
using System.Threading.Tasks;
using Cabinet.Dtos.Internal.Request;

namespace TurnTable.InternalServices.NameSearchExamination {
    
    public interface INameSearchExaminationService {
        Task<int> ChangeNameStatusAsync(int nameId, int status);
        Task<int> FinishExaminationAsync(int nameSearchId);
        Task<List<NameRequestDto>> GetNamesThatStartWithAsync(string searchQuery);
        Task<List<NameRequestDto>> GetNamesThatContainAsync(string searchQuery);
        Task<List<NameRequestDto>> GetNamesThatEndsWithAsync(string searchQuery);
    }
}