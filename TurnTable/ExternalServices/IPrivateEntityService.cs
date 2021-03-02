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
        Task<ApplicationResponseDto> CreateApplicationAsync(Guid user, int nameId, string industrySector);

        Task<ApplicationResponseDto> InsertOfficeAsync(Guid user, NewPrivateEntityOfficeRequestDto dto);

        Task<ApplicationResponseDto> InsertMemorandumOfAssociationAsync(Guid user, NewMemorandumRequestDto dto);

        Task<ApplicationResponseDto> InsertMemorandumObjectsAsync(Guid user,
            NewMemorandumOfAssociationObjectsRequestDto dto);

        Task<ApplicationResponseDto> InsertArticlesOfAssociationAsync(Guid user, NewArticleOfAssociationRequestDto dto);

        Task<ApplicationResponseDto> InsertShareClauseAsync(Guid user, NewShareClausesRequestDto dto);

        Task<ApplicationResponseDto> InsertMembersAsync(Guid user, NewShareHoldersRequestDto dto);
        Task<int> SubmitApplicationAsync(Guid user, int applicationId);
        Task<ApplicationResponseDto> ResubmitApplicationAsync(Guid user, int applicationId);
    }
}