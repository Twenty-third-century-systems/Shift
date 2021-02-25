using System.Collections.Generic;
using System.Threading.Tasks;
using Cabinet.Dtos.Internal.Request;
using Cabinet.Dtos.Internal.Response;

namespace TurnTable.InternalServices {
    public interface IApplicationService {
        Task<List<UnallocatedApplicationResponseDto>> GetAllUnAllocatedApplicationsAsync(int sortingOffice);
        Task<int> AllocateTasksAsync(NewTaskAllocationRequestDto dto);
    }
}