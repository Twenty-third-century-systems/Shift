using System.Threading.Tasks;

namespace TurnTable.InternalServices {
    
    public interface IPrivateEntityExaminationService {
        Task FinishExamination(int applicationId);
        Task<int> RaiseQuery(int applicationId, int step, string comment);
    }
}