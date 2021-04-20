using System.Collections.Generic;
using System.Threading.Tasks;
using Cabinet.Dtos.Internal.Response;

namespace Drinkers.InternalApiClients.Applications {
    public interface IApplicationsApiClientService {
        Task<List<AllocatedPrivateEntityTaskApplicationResponseDto>> ApplicationsPendingApprovalAsync(int sortingOffice);
        Task<bool> ApproveAsync(int applicationId);
    }
}