using System.Collections.Generic;
using System.Threading.Tasks;
using Cabinet.Dtos.Internal.Request;
using Cabinet.Dtos.Internal.Response;

namespace Drinkers.InternalClients.Applications {
    public interface IApplicationsApiClientService {
        Task<List<AllocatedPrivateEntityTaskApplicationResponseDto>> ApplicationsPendingApproval(int sortingOffice);
        Task<bool> Approve(int applicationId);
    }
}