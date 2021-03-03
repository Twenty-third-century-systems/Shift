using System.Net.Http;
using Microsoft.AspNetCore.Builder;

namespace DanceFlow.Client {
    public class ApiClientService : IApiClientService {
        private readonly HttpClient _client;

        public ApiClientService(HttpClient client)
        {
            _client = client;
        }
    }
}