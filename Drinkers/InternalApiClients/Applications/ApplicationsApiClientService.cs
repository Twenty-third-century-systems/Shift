using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Cabinet.Dtos.Internal.Response;

namespace Drinkers.InternalApiClients.Applications {
    public class ApplicationsApiClientService : IApplicationsApiClientService {
        private readonly HttpClient _client;

        public ApplicationsApiClientService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<AllocatedPrivateEntityTaskApplicationResponseDto>> ApplicationsPendingApprovalAsync(int sortingOffice)
        {            
            var response = await _client.GetAsync($"applications/{sortingOffice}/pending/approval");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<List<AllocatedPrivateEntityTaskApplicationResponseDto>>();
            return null;
        }

        public async Task<bool> ApproveAsync(int applicationId)
        {
            var response = await _client.PatchAsync($"applications/{applicationId}/approve",null);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }
    }
}