using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cabinet.Dtos.Internal.Request;
using Cabinet.Dtos.Internal.Response;

namespace Drinkers.InternalApiClients.Task {
    public interface ITaskApiClientService {
        Task<List<UnallocatedApplicationResponseDto>> GetAllUnallocatedApplicationsAsync(int office);
        Task<int?> PostMultipleApplicationTaskAsync(NewTaskAllocationRequestDto dto);
        Task<List<AllocatedTaskResponseDto>> GetAllocatedTasksAsync(Guid examinerId);
        Task<List<AllocatedNameSearchTaskApplicationResponseDto>> GetAllocatedNameSearchTaskApplicationsAsync(int taskId);
        Task<bool> FinishTaskAsync(int taskId);
        Task<List<AllocatedPrivateEntityTaskApplicationResponseDto>> GetPvtEntityTaskApplicationAsync(int taskId);
    }
}