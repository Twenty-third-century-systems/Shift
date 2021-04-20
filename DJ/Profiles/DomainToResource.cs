using System.Linq;
using AutoMapper;
using Cabinet.Dtos.Internal.Request;
using Cabinet.Dtos.Internal.Response;
using Fridge.Constants;
using Fridge.Models;
using Fridge.Models.Main;

namespace DJ.Profiles {
    public class DomainToResource : Profile {
        public DomainToResource()
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

            // Application => SubmittedApplicationResponseDto
            CreateMap<Application, SubmittedApplicationResponseDto>();

            // PrivateEntity => AllocatedPrivateEntityTaskApplicationResponseDto
            CreateMap<PrivateEntity, AllocatedPrivateEntityTaskApplicationResponseDto>()
                .ForMember(dest => dest.Application, op =>
                    op.MapFrom(src => src.CurrentApplication))
                .ForMember(dest => dest.Name,
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

            // PersonSubscription => TaskPrivateEntityShareholderSubscriptionResponseDto
            CreateMap<PersonSubscription, TaskPrivateEntityShareholderSubscriptionResponseDto>()
                .ForMember(dest => dest.Title, op =>
                    op.MapFrom(src => src.ShareClause.Title))
                .ForMember(dest => dest.Amount, op =>
                    op.MapFrom(src => src.AmountOfSharesSubscribed));

            // PrivateEntitySubscriptions => TaskPrivateEntityShareholderSubscriptionResponseDto
            CreateMap<PrivateEntitySubscription, TaskPrivateEntityShareholderSubscriptionResponseDto>()
                .ForMember(dest => dest.Title, op =>
                    op.MapFrom(src => src.ShareClause.Title));

            // PrivateEntitySubscriptions => TaskPrivateEntityShareholderSubscriptionResponseDto
            CreateMap<ForeignEntitySubscription, TaskPrivateEntityShareholderSubscriptionResponseDto>()
                .ForMember(dest => dest.Title, op =>
                    op.MapFrom(src => src.ShareClause.Title))
                .ForMember(dest => dest.Amount, op =>
                    op.MapFrom(src => src.AmountOfSharesSubscribed));

            // MemorandumObject => TaskPrivateEntityMemorandumObjectResponseDto
            CreateMap<MemorandumOfAssociation, TaskPrivateEntityMemorandumObjectResponseDto>();


            // PrivateEntityOwner => TaskPrivateEntityShareHolderResponseDto
            CreateMap<ShareHolder, TaskPrivateEntityShareHolderResponseDto>()
                .ForMember(dest => dest.FullName,
                    op => op.MapFrom(src => $"{src.Surname} {src.Names}"))
                .ForMember(dest => dest.Country, op =>
                    op.MapFrom(src => src.Country.Name));

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

            // PrivateEntity => TaskShareHoldingEntityRequestDto
            CreateMap<PrivateEntity, TaskShareHoldingEntityRequestDto>()
                .ForMember(dest => dest.Name, op =>
                    op.MapFrom(src => src.Name.Value));

            // ForeignEntity => TaskShareHoldingEntityRequestDto
            CreateMap<ForeignEntity, TaskShareHoldingEntityRequestDto>()
                .ForMember(dest => dest.Name, op =>
                    op.MapFrom(src => src.ForeignEntityName))
                .ForMember(dest => dest.Reference, op =>
                    op.MapFrom(src => src.CompanyReference));


            // Director => TaskPrivateEntityPersonResponseDto
            CreateMap<Director, TaskPrivateEntityPersonResponseDto>()
                .ForMember(dest => dest.FullName,
                    op => op.MapFrom(src => $"{src.Surname} {src.Names}"))
                .ForMember(dest => dest.Country, op =>
                    op.MapFrom(src => src.Country.Name));


            // Director => TaskPrivateEntityPersonResponseDto
            CreateMap<Secretary, TaskPrivateEntityPersonResponseDto>()
                .ForMember(dest => dest.FullName,
                    op => op.MapFrom(src => $"{src.Surname} {src.Names}"))
                .ForMember(dest => dest.Country, op =>
                    op.MapFrom(src => src.Country.Name));
        }
    }
}