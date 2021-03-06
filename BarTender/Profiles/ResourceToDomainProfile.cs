using AutoMapper;
using Cabinet.Dtos.External.Request;
using Fridge.Models;
using Fridge.Models.Main;

namespace BarTender.Profiles {
    public class ResourceToDomainProfile : Profile {
        public ResourceToDomainProfile()
        {
            // NewNameSearchRequestDto => NameSearch
            CreateMap<NewNameSearchRequestDto, NameSearch>()
                .ForMember(
                    dest => dest.Service,
                    op => op.MapFrom(src => src.ServiceId));

            //SuggestedEntityNameRequestDto => EntityName
            CreateMap<SuggestedEntityNameRequestDto, EntityName>();

            // NewPrivateEntityAddressRequestDto => Address
            CreateMap<NewPrivateEntityAddressRequestDto, Address>();

            // NewPrivateEntityOfficeRequestDto => Office
            CreateMap<NewPrivateEntityOfficeRequestDto, Office>();

            // NewMemorandumRequestDto => MemorandumOfAssociation
            CreateMap<NewLiabilityClauseRequestDto, MemorandumOfAssociation>();

            // NewShareClauseRequestDto => ShareClause
            CreateMap<NewShareClauseRequestDto, ShareClause>();

            // NewMemorandumOfAssociationObjectRequestDto => MemorandumOfAssociationObject
            CreateMap<NewMemorandumOfAssociationObjectRequestDto, MemorandumOfAssociationObject>();

            // NewAmendedArticleRequestDto => AmendedArticle
            CreateMap<NewAmendedArticleRequestDto, AmendedArticle>();

            // NewArticleOfAssociationRequestDto => ArticlesOfAssociation
            CreateMap<NewArticleOfAssociationRequestDto, ArticlesOfAssociation>();

            // NewShareHolderRequestDto => PrivateEntityOwner
            CreateMap<NewShareHolderRequestDto, ShareHolder>();

            // NewShareHoldingEntityRequestDto => ShareholdingForeignEntity
            CreateMap<NewShareHoldingEntityRequestDto, ForeignEntity>()
                .ForMember(dest => dest.ForeignEntityName, op =>
                    op.MapFrom(src => src.Name));
            
            // NewDirectorSecretaryRequestDto => Director
            CreateMap<NewDirectorSecretaryRequestDto, Director>();
            
            // NewDirectorSecretaryRequestDto => Secretary
            CreateMap<NewDirectorSecretaryRequestDto, Secretary>();
            
            
        }
    }
}