using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Cabinet.Dtos.Internal.Request;
using Cabinet.Dtos.Internal.Response;

namespace Drinkers.InternalClients.Task {
    public class TaskApiClientService : ITaskApiClientService {
        private readonly HttpClient _client;

        public TaskApiClientService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<UnallocatedApplicationResponseDto>> GetAllUnallocatedApplicationsAsync(int office)
        {
            var response = await _client.GetAsync($"applications/all/{office}");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<List<UnallocatedApplicationResponseDto>>();
            return null;
        }

        public async Task<int?> PostMultipleApplicationTaskAsync(NewTaskAllocationRequestDto dto)
        {
            var response = await _client.PostAsJsonAsync("applications/allocate", dto);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<int>();
            }

            return null;
        }

        public async Task<List<AllocatedTaskResponseDto>> GetAllocatedTasksAsync(Guid examinerId)
        {
            var response = await _client.GetAsync($"tasks/{examinerId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<List<AllocatedTaskResponseDto>>();
            }

            return null;
        }

        public async Task<List<AllocatedNameSearchTaskApplicationResponseDto>> GetAllocatedTaskApplicationsAsync(
            int taskId)
        {
            var response = await _client.GetAsync($"tasks/{taskId}/ns");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<List<AllocatedNameSearchTaskApplicationResponseDto>>();
            return null;
        }

        public async Task<bool> FinishTaskAsync(int taskId)
        {
            var response = await _client.SendAsync(new HttpRequestMessage(HttpMethod.Head, $"tasks/{taskId}/f"));
            if(response.IsSuccessStatusCode)
                return true;
            return false;
        }
    }
}