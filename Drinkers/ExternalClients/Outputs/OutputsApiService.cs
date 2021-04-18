using System.Net.Http;
using System.Threading.Tasks;
using Cabinet.Dtos.External.Response;

namespace Drinkers.ExternalClients.Outputs {
    public class OutputsApiService : IOutputsApiService {
        private readonly HttpClient _client;

        public OutputsApiService(HttpClient client)
        {
            _client = client;
        }

        public async Task<ReservedNameRequestDto> GetNameSearchInfoForDoc(int applicationId)
        {
            var response = await _client.GetAsync($"outputs/ns/{applicationId}/sum");
            if(response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<ReservedNameRequestDto>();
            return null;
        }

        public async Task<int> PrivateEntityNameSearchSummary(string applicationId)
        {
            var response = await _client.GetAsync($"outputs/pvt/{applicationId}/ns/sum");
            if(response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<int>();
            return 0;
        }

        public async Task<PrivateEntitySummaryRequestDto> PrivateEntitySummary(int applicationId)
        {
            var response = await _client.GetAsync($"outputs/pvt/{applicationId}/sum");
            if(response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<PrivateEntitySummaryRequestDto>();
            return null;
        }

        public async Task<RegisteredPrivateEntityRequestDto> RegisteredPrivateEntity(int applicationId)
        {
            var response = await _client.GetAsync($"outputs/pvt/cert/{applicationId}");
            if(response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<RegisteredPrivateEntityRequestDto>();
            return null;
        }
    }
}