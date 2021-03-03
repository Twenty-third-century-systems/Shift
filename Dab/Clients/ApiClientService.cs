using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Cabinet.Dtos.External.Request;
using Cabinet.Dtos.External.Response;

namespace Dab.Clients {
    public class ApiClientService : IApiClientService {
        private readonly HttpClient _client;

        public ApiClientService(HttpClient client)
        {
            _client = client;
        }

        public async Task<ExternalUserDashboardRequestDto> GetDashBoardDefaultsAsync()
        {
            var response = await _client.GetAsync("Dashboard");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<ExternalUserDashboardRequestDto>();
            }

            return null;
        }

        public async Task<NameSearchDefaultsRequestDto> GetNameSearchDefaultsAsync()
        {
            var response = await _client.GetAsync("name/defaults");
            if (response.IsSuccessStatusCode)
            {
                var nameSearchDefaults = await response.Content.ReadAsAsync<NameSearchDefaultsRequestDto>();
                return nameSearchDefaults;
            }

            return null;
        }

        public async Task<SubmittedNameSearchResponseDto> PostNewNameSearchAsync(
            NewNameSearchRequestDto newNameSearchRequestDto)
        {
            var response =
                await _client.PostAsJsonAsync("name/submit", newNameSearchRequestDto);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<SubmittedNameSearchResponseDto>();
            }

            return null;
        }

        public async Task<bool> GetNameAvailabilityAsync(string nameToSend)
        {
            var response =
                await _client.SendAsync(new HttpRequestMessage(HttpMethod.Head, $"name/{nameToSend}/availability"));
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }
    }
}