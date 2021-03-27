using System.Threading.Tasks;
using Cabinet.Dtos.External.Request;
using Cabinet.Dtos.External.Response;

namespace Drinkers.ExternalClients.PrivateEntity {
    public interface IPrivateEntityApiClientService {
        Task<ApplicationResponseDto> NewPrivateEntityAsync(int nameId);
        Task<ApplicationResponseDto> NewPrivateEntityOffice(NewPrivateEntityOfficeRequestDto dto);        
        Task<ApplicationResponseDto> NewDirectors(NewDirectorsRequestDto dto);     
        Task<ApplicationResponseDto> NewSecretary(NewSecretaryRequestDto dto);
        Task<ApplicationResponseDto> Objectives(NewMemorandumOfAssociationObjectsRequestDto dto);
        Task<ApplicationResponseDto> LiabilityClause(NewLiabilityClauseRequestDto dto);
        Task<ApplicationResponseDto> ShareClauses(NewShareClausesRequestDto dto);
        Task<ApplicationResponseDto> ShareHolders(NewShareHoldersRequestDto dto);
        Task<ApplicationResponseDto> TableOfArticles(NewArticleOfAssociationRequestDto dto);
        Task<ApplicationResponseDto> AmendedArticles(NewAmendedArticlesRequestDto dto);
        Task<bool> Finish(int applicationId);
    }
}