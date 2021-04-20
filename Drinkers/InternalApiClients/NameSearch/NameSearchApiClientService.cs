using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Cabinet.Dtos.Internal.Request;

namespace Drinkers.InternalApiClients.NameSearch {
    public class NameSearchApiClientService : INameSearchApiClientService {
        private readonly HttpClient _client;

        public NameSearchApiClientService(HttpClient client)
        {
            _client = client;
        }
        
        

        public async Task<bool> ChangeNameStatusAsync(int nameId, int status)
        {
            var response = await _client.PatchAsync($"ex/name/{nameId}/{status}", null);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> FinishNameSearchExaminationAsync(int nameSearchId)
        {
            var response = await _client.PatchAsync($"/api/ex/name/f/{nameSearchId}", null);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task<List<NameRequestDto>> GetNamesThatContainAsync(string suggestedName)
        {
            var response = await _client.GetAsync($"ex/name/{suggestedName}/contain");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<List<NameRequestDto>>();
            return null;
        }

        public async Task<List<NameRequestDto>> GetNamesThatStartWithAsync(string suggestedName)
        {
            var response = await _client.GetAsync($"ex/name/{suggestedName}/starts");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<List<NameRequestDto>>();
            return null;
        }

        public async Task<List<NameRequestDto>> GetNamesThatEndWithAsync(string suggestedName)
        {
            var response = await _client.GetAsync($"ex/name/{suggestedName}/ends");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<List<NameRequestDto>>();
            return null;
        }
    }
}