using System.Net.Http;
using System.Threading.Tasks;
using Cabinet.Dtos.External.Request;
using Cabinet.Dtos.External.Response;

namespace Drinkers.ExternalClients.PrivateEntity {
    public class PrivateEntityApiClientService : IPrivateEntityApiClientService {
        private readonly HttpClient _client;

        public PrivateEntityApiClientService(HttpClient client)
        {
            _client = client;
        }

        public async Task<ApplicationResponseDto> NewPrivateEntityAsync(int nameId)
        {
            var response = await _client.PostAsync($"entity/name?nameId={nameId}", null);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<ApplicationResponseDto>();
            return null;
        }

        public async Task<ApplicationResponseDto> NewPrivateEntityOffice(NewPrivateEntityOfficeRequestDto dto)
        {
            var response = await _client.PostAsJsonAsync("entity/office", dto);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<ApplicationResponseDto>();
            return null;
        }

        public async Task<ApplicationResponseDto> NewDirectors(NewDirectorsRequestDto dto)
        {
            var response = await _client.PostAsJsonAsync("entity/directors", dto);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<ApplicationResponseDto>();
            return null;
        }

        public async Task<ApplicationResponseDto> NewSecretary(NewSecretaryRequestDto dto)
        {
            var response = await _client.PostAsJsonAsync("entity/secretary", dto);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<ApplicationResponseDto>();
            return null;
        }

        public async Task<ApplicationResponseDto> Objectives(NewMemorandumOfAssociationObjectsRequestDto dto)
        {
            var response = await _client.PostAsJsonAsync("entity/memorandum/objects", dto);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<ApplicationResponseDto>();
            return null;
        }

        public async Task<ApplicationResponseDto> LiabilityClause(NewLiabilityClauseRequestDto dto)
        {
            var response = await _client.PostAsJsonAsync("entity/liability/clause", dto);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<ApplicationResponseDto>();
            return null;
        }

        public async Task<ApplicationResponseDto> ShareClauses(NewShareClausesRequestDto dto)
        {
            var response = await _client.PostAsJsonAsync("entity/share/clause", dto);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<ApplicationResponseDto>();
            return null;
        }

        public async Task<ApplicationResponseDto> ShareHolders(NewShareHoldersRequestDto dto)
        {
            var response = await _client.PostAsJsonAsync("entity/shareholders", dto);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<ApplicationResponseDto>();
            return null;
        }

        public async Task<ApplicationResponseDto> TableOfArticles(NewArticleOfAssociationRequestDto dto)
        {
            var response = await _client.PostAsJsonAsync("entity/articles", dto);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<ApplicationResponseDto>();
            return null;
        }

        public async Task<ApplicationResponseDto> AmendedArticles(NewAmendedArticlesRequestDto dto)
        {
            var response = await _client.PostAsJsonAsync("entity/amended/articles", dto);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<ApplicationResponseDto>();
            return null;
        }

        public async Task<bool> Finish(int applicationId)
        {
            var response =
                await _client.SendAsync(new HttpRequestMessage(HttpMethod.Head,
                    $"entity/finish?applicationId={applicationId}"));
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }
    }
}