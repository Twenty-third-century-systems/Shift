using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Cabinet.Dtos.Internal.Request;
using Cabinet.Dtos.Internal.Response;

namespace Drinkers.InternalClients.Applications {
    public class ApplicationsApiClientService : IApplicationsApiClientService {
        private readonly HttpClient _client;

        public ApplicationsApiClientService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<AllocatedPrivateEntityTaskApplicationResponseDto>> ApplicationsPendingApproval(int sortingOffice)
        {            
            var response = await _client.GetAsync($"applications/{sortingOffice}/pending/approval");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<List<AllocatedPrivateEntityTaskApplicationResponseDto>>();
            return null;
        }

        public async Task<bool> Approve(int applicationId)
        {
            var response = await _client.GetAsync($"applications/{applicationId}/approve");
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }
    }
}