using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cabinet.Dtos.Internal.Request;
using Cabinet.Dtos.Internal.Response;

namespace DanceFlow.Client {
    public interface IApiClientService {
        Task<List<UnallocatedApplicationResponseDto>> GetAllUnallocatedApplicationsAsync(int office);
        Task<int?> PostMultipleApplicationTaskAsync(NewTaskAllocationRequestDto dto);
        Task<List<AllocatedTaskResponseDto>> GetAllocatedTasksAsync(Guid examinerId);
        Task<List<AllocatedNameSearchTaskApplicationResponseDto>> GetAllocatedTaskApplicationsAsync(int taskId);
        Task<bool> ChangeNameStatusAsync(int nameId, int status);
        Task<bool> FinishNameSearchExaminationAsync(int nameSearchId);
        Task<List<NameRequestDto>> GetNamesThatContainAsync(string suggestedName);
        Task<List<NameRequestDto>> GetNamesThatStartWithAsync(string suggestedName);
        Task<List<NameRequestDto>> GetNamesThatEndWithAsync(string suggestedName);
        Task<bool> FinishTaskAsync(int taskId);
    }
}