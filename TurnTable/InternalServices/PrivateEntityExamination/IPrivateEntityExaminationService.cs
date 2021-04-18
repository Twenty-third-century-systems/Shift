using System.Threading.Tasks;
using Cabinet.Dtos.Internal.Request;

namespace TurnTable.InternalServices.PrivateEntityExamination {
    
    public interface IPrivateEntityExaminationService {
        Task<int> FinishExaminationAsync(int applicationId);
        Task<int> RaiseQuery(int applicationId, int step, string comment);
        Task<int> RaiseQueryAsync(RaisedQueryRequestDto dto);
    }
}