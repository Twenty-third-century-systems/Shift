using System.Net.Http;
using System.Threading.Tasks;
using Cabinet.Dtos.External.Request;
using Cabinet.Dtos.External.Response;

namespace Dab.Clients.PrivateEntity {
    public class PrivateEntityApiClientService : IPrivateEntityApiClientService {
        private readonly HttpClient _client;

        public PrivateEntityApiClientService(HttpClient client)
        {
            _client = client;
        }
        public async Task<ApplicationResponseDto> NewPrivateEntityAsync(int nameId)
        {
            var response = await _client.PostAsync($"entity/name?nameId={nameId}", null);
            if(response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<ApplicationResponseDto>();
            return null;
        }

        public async Task<ApplicationResponseDto> NewPrivateEntityOffice(NewPrivateEntityOfficeRequestDto dto)
        {
            var response = await _client.PostAsJsonAsync("entity/office", dto);
            if(response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<ApplicationResponseDto>();
            return null;
        }

        public async Task<ApplicationResponseDto> NewDirectors(NewDirectorsRequestDto dto)
        {
            var response = await _client.PostAsJsonAsync("entity/directors", dto);
            if(response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<ApplicationResponseDto>();
            return null;
        }

        public async Task<ApplicationResponseDto> NewSecretary(NewSecretaryRequestDto dto)
        {
            var response = await _client.PostAsJsonAsync("entity/secretary", dto);
            if(response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<ApplicationResponseDto>();
            return null;
        }
    }
}