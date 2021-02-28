using System.Collections.Generic;
using System.Threading.Tasks;
using Fridge.Models;

namespace TurnTable.InternalServices {
    
    public interface INameSearchExaminationService {
        Task<int> ChangeNameStatusAsync(int nameId, int status);
        Task<int> FinishExaminationAsync(int nameSearchId);
        Task<List<EntityName>> GetNamesThatStartWithAsync(string searchQuery);
        Task<List<EntityName>> GetNamesThatContainAsync(string searchQuery);
        Task<List<EntityName>> GetNamesThatEndsWithAsync(string searchQuery);
    }
}