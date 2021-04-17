using System.Net.Http;
using System.Threading.Tasks;
using Cabinet.Dtos.Internal.Request;

namespace Drinkers.InternalClients.PvtEntity {
    public class PvtEntityApiClientService: IPvtEntityApiClientService {
        private readonly HttpClient _client;

        public PvtEntityApiClientService(HttpClient client)
        {
            _client = client;
        }
        
        

        public async Task<bool> Query(RaisedQueryRequestDto dto)
        {
            var response = await _client.PostAsJsonAsync("ex/pvt/q", dto);
            if(response.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task<bool> FinishAsync(int applicationId)
        {
            var response = await _client.PatchAsync($"ex/pvt/f/{applicationId}", null);
            if(response.IsSuccessStatusCode)
                return true;
            return false;
        }
    }
}