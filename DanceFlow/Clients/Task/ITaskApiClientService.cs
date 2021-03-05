using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cabinet.Dtos.Internal.Request;
using Cabinet.Dtos.Internal.Response;

namespace DanceFlow.Clients.Task {
    public interface ITaskApiClientService {
        Task<List<UnallocatedApplicationResponseDto>> GetAllUnallocatedApplicationsAsync(int office);
        Task<int?> PostMultipleApplicationTaskAsync(NewTaskAllocationRequestDto dto);
        Task<List<AllocatedTaskResponseDto>> GetAllocatedTasksAsync(Guid examinerId);
        Task<List<AllocatedNameSearchTaskApplicationResponseDto>> GetAllocatedTaskApplicationsAsync(int taskId);
        Task<bool> FinishTaskAsync(int taskId);
    }
}