using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cabinet.Dtos.External.Request;
using Cabinet.Dtos.External.Response;
using Cabinet.Dtos.Internal.Request;
using Fridge.Models;

namespace TurnTable.ExternalServices {
    public interface IPrivateEntityService {
        Task<List<RegisteredNameResponseDto>> GetRegisteredNamesAsync(Guid user);
        Task<ApplicationResponseDto> CreateApplicationAsync(Guid user, int nameId);

        Task<ApplicationResponseDto> InsertOfficeAsync(Guid user, NewPrivateEntityOfficeRequestDto dto);

        Task<ApplicationResponseDto> InsertLiabilityClauseAsync(Guid user, NewLiabilityClauseRequestDto dto);

        Task<ApplicationResponseDto> InsertMemorandumObjectsAsync(Guid user,
            NewMemorandumOfAssociationObjectsRequestDto dto);

        Task<ApplicationResponseDto> InsertArticlesOfAssociationAsync(Guid user, NewArticleOfAssociationRequestDto dto);

        Task<ApplicationResponseDto> InsertShareClauseAsync(Guid user, NewShareClausesRequestDto dto);

        Task<ApplicationResponseDto> InsertMembersAsync(Guid user, NewShareHoldersRequestDto dto);
        Task<int> FinishApplicationAsync(Guid user, int applicationId);
        Task<ApplicationResponseDto> ResubmitApplicationAsync(Guid user, int applicationId);
        Task<ApplicationResponseDto> InsertAmendedArticles(Guid user, NewAmendedArticlesRequestDto dto);
        Task<ApplicationResponseDto> InsertDirectors(Guid user, NewDirectorsRequestDto dto);
        Task<ApplicationResponseDto> InsertSecretary(Guid user, NewSecretaryRequestDto dto);
    }
}