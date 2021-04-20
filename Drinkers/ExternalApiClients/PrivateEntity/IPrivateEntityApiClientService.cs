using System.Threading.Tasks;
using Cabinet.Dtos.External.Request;
using Cabinet.Dtos.External.Response;

namespace Drinkers.ExternalApiClients.PrivateEntity {
    public interface IPrivateEntityApiClientService {
        Task<ApplicationResponseDto> NewPrivateEntityAsync(int nameId);
        Task<ApplicationResponseDto> NewPrivateEntityOfficeAsync(NewPrivateEntityOfficeRequestDto dto);        
        Task<ApplicationResponseDto> NewDirectorsAsync(NewDirectorsRequestDto dto);     
        Task<ApplicationResponseDto> NewSecretaryAsync(NewSecretaryRequestDto dto);
        Task<ApplicationResponseDto> ObjectivesAsync(NewMemorandumOfAssociationObjectsRequestDto dto);
        Task<ApplicationResponseDto> LiabilityClauseAsync(NewLiabilityClauseRequestDto dto);
        Task<ApplicationResponseDto> ShareClausesAsync(NewShareClausesRequestDto dto);
        Task<ApplicationResponseDto> ShareHoldersAsync(NewShareHoldersRequestDto dto);
        Task<ApplicationResponseDto> TableOfArticlesAsync(NewArticleOfAssociationRequestDto dto);
        Task<ApplicationResponseDto> AmendedArticlesAsync(NewAmendedArticlesRequestDto dto);
        Task<bool> FinishAsync(int applicationId);
    }
}