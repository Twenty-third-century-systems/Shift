using System.Linq;
using AutoMapper;
using Cabinet.Dtos.Internal.Request;
using Cabinet.Dtos.Internal.Response;
using Fridge.Constants;
using Fridge.Models;

namespace DJ.Profiles {
    public class DomainToResourceProfiles : Profile {
        public DomainToResourceProfiles()
        {
            // Application => UnallocatedApplicationResponseDto
            CreateMap<Application, UnallocatedApplicationResponseDto>();

            // ExaminerTask => AllocatedTaskResponseDto
            CreateMap<ExaminationTask, AllocatedTaskResponseDto>()
                .ForMember(
                    dest => dest.ApplicationCount,
                    op => op.MapFrom(src => src.Applications.Count))
                .ForMember(
                    dest => dest.Service,
                    op => op.MapFrom(src => src.Applications.First().Service));

            // Application => AllocatedTaskNameSearchApplicationResponseDto
            CreateMap<Application, AllocatedNameSearchTaskApplicationResponseDto>()
                .ForMember(dest => dest.Examined,
                    op => op.MapFrom(src => src.Status == EApplicationStatus.Examined));

            // NameSearch => TaskNameSearchResponseDto
            CreateMap<NameSearch, TaskNameSearchResponseDto>();

            // EntityName => TaskNameResponseDto
            CreateMap<EntityName, TaskNameSearchNameResponseDto>();

            // Application => AllocatedPrivateEntityTaskApplicationResponseDto
            CreateMap<Application, AllocatedPrivateEntityTaskApplicationResponseDto>();

            // PrivateEntity => TaskPrivateEntityResponseDto
            CreateMap<PrivateEntity, TaskPrivateEntityResponseDto>()
                .ForMember(
                    dest => dest.Name,
                    op => op.MapFrom(src =>
                        src.Name.Value));

            // Office => TaskPrivateEntityOfficeResponseDto
            CreateMap<Office, TaskPrivateEntityOfficeResponseDto>()
                .ForMember(
                    dest => dest.PhysicalAddress,
                    op => op.MapFrom(src => src.Address.PhysicalAddress))
                .ForMember(
                    dest => dest.PostalAddress,
                    op => op.MapFrom(src => src.Address.PostalAddress))
                .ForMember(
                    dest => dest.CityTown,
                    op => op.MapFrom(src => src.Address.CityTown));

            // ArticlesOfAssociation => TaskPrivateEntityArticlesOfAssociationResponseDto
            CreateMap<ArticlesOfAssociation, TaskPrivateEntityArticlesOfAssociationResponseDto>()
                .ForMember(dest => dest.TableOfArticles,
                    op => op.MapFrom(src => src.TableOfArticles.ToString()));

            // AmendedArticle => TaskPrivateEntityAmendedArticleDto
            CreateMap<AmendedArticle, TaskPrivateEntityAmendedArticleDto>();

            // MemorandumOfAssociation => TaskPrivateEntityMemorandumResponseDto
            CreateMap<MemorandumOfAssociation, TaskPrivateEntityMemorandumResponseDto>();

            // MemorandumObject => TaskPrivateEntityMemorandumObjectResponseDto
            CreateMap<MemorandumOfAssociationObject, TaskPrivateEntityMemorandumObjectResponseDto>();

            // ShareClause => TaskPrivateEntityShareClauseResponseDto
            CreateMap<ShareClause, TaskPrivateEntityShareClauseResponseDto>()
                .ForMember(
                    dest => dest.Value,
                    op => op.MapFrom(src => $"{src.TotalNumberOfShares} {src.Title} shares"));

            // MemorandumObject => TaskPrivateEntityMemorandumObjectResponseDto
            CreateMap<MemorandumOfAssociation, TaskPrivateEntityMemorandumObjectResponseDto>();

            // PrivateEntityOwner => TaskPrivateEntityShareHolderResponseDto
            CreateMap<ShareHolder, TaskPrivateEntityShareHolderResponseDto>()
                .ForMember(
                    dest => dest.FullName,
                    op => op.MapFrom(src => $"{src.Surname} {src.Names}"));

            // PrivateEntityOwnerHasShareClause => TaskPrivateEntityShareholderSubscriptionResponseDto

            // ShareholdingForeignEntity, TaskPrivateEntityForeignEntityShareHoldersDto
            CreateMap<ForeignEntity, TaskPrivateEntityForeignEntityShareHoldersDto>();

            // EntityName => NameRequestDto
            CreateMap<EntityName, NameRequestDto>()
                .ForMember(
                    dest => dest.Name,
                    op => op.MapFrom(src => src.Value))
                .ForMember(dest => dest.DateSubmitted,
                    op => op.MapFrom(src => src.NameSearch.Application.DateSubmitted.ToString("d")))
                .ForMember(dest => dest.TypeOfBusiness,
                    op => op.MapFrom(src => src.NameSearch.Service));
        }
    }
}