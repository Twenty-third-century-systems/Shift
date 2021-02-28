using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cabinet.Dtos.Internal.Request;
using Cabinet.Dtos.Internal.Response;
using Fridge.Models;

namespace TurnTable.InternalServices {
    public interface ITaskService {
        Task<List<UnallocatedApplicationResponseDto>> GetAllUnAllocatedApplicationsAsync(int sortingOffice);
        Task<int> AllocateTasksAsync(NewTaskAllocationRequestDto dto);
        Task<List<AllocatedTaskResponseDto>> GetAllocatedTasksAsync(Guid examiner);

        Task<List<AllocatedNameSearchTaskApplicationResponseDto>> GetNameSearchTaskApplicationsAsync(int taskId);
        Task<AllocatedPrivateEntityTaskApplicationResponseDto> GetPrivateEntityTaskApplicationAsync(int taskId);
    }
}