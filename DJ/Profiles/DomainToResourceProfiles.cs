using System.Linq;
using AutoMapper;
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
            CreateMap<Application, AllocatedNameSearchTaskApplicationResponseDto>();

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
                        src.NameSearchApplicationApplication.NameSearch.Names
                            .SingleOrDefault(n => n.Status.Equals(ENameStatus.Used)).Value));

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
                    op => op.MapFrom(src => $"{src.Title} @ ${src.NominalValue} each"));
            
            // MemorandumObject => TaskPrivateEntityMemorandumObjectResponseDto
            CreateMap<MemorandumOfAssociation, TaskPrivateEntityMemorandumObjectResponseDto>();

            // PrivateEntityOwner => TaskPrivateEntityShareHolderResponseDto
            CreateMap<Person, TaskPrivateEntityShareHolderResponseDto>()
                .ForMember(
                    dest => dest.FullName,
                    op => op.MapFrom(src => $"{src.Surname} {src.Names}"));

            // PrivateEntityOwnerHasShareClause => TaskPrivateEntityShareholderSubscriptionResponseDto
            CreateMap<PrivateEntityOwnerHasShareClause, TaskPrivateEntityShareholderSubscriptionResponseDto>()
                .ForMember(
                    dest => dest.Title,
                    op => op.MapFrom(src => $"{src.ShareClauseClass.Title} @ ${src.ShareClauseClass.NominalValue}"));

            // ShareholdingForeignEntity, TaskPrivateEntityForeignEntityShareHoldersDto
            CreateMap<ShareholdingForeignEntity, TaskPrivateEntityForeignEntityShareHoldersDto>();
        }
    }
}