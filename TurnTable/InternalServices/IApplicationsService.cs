using System.Collections.Generic;
using System.Threading.Tasks;
using Cabinet.Dtos.Internal.Response;

namespace TurnTable.InternalServices {
    public interface IApplicationsService {
        Task<List<AllocatedPrivateEntityTaskApplicationResponseDto>> GetApplicationsPendingApproval(int sortingOffice);
        Task<int> ApprovePrivateEntityApplication(int applicationId);
    }
}