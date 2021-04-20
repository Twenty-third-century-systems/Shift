using AutoMapper;
using Cabinet.Dtos.External.Request;

namespace Dab.Profiles {
    public class ResourceToResource : Profile {
        public ResourceToResource()
        {
            // NewNameSearchFormRequestDto => NewNameSearchRequestDto
            CreateMap<NewNameSearchFormRequestDto, NewNameSearchRequestDto>();


            // PrivateOfficeAddressRequestDto => NewPrivateEntityAddressRequestDto
            CreateMap<PrivateOfficeAddressRequestDto, NewPrivateEntityAddressRequestDto>();


            // PrivateOfficeAddressRequestDto => NewPrivateEntityOfficeRequestDto
            CreateMap<PrivateOfficeAddressRequestDto, NewPrivateEntityOfficeRequestDto>();


            // LiabilityClauseDto => NewLiabilityClauseRequestDto
            CreateMap<LiabilityClauseDto, NewLiabilityClauseRequestDto>()
                .ForMember(dest => dest.LiabilityClause,
                    op => op.MapFrom(src => src.Clause.LiabilityClause));

            // NomineeBeneficiaryRequestDto => NewShareHolderRequestDto
            CreateMap<NomineeBeneficiaryRequestDto, NewShareHolderRequestDto>()
                .ForMember(dest => dest.CountryCode,
                    op => op.MapFrom(src => src.NomineeCountryCode))
                .ForMember(dest => dest.Surname,
                    op => op.MapFrom(src => src.NomineeSurname))
                .ForMember(dest => dest.Names,
                    op => op.MapFrom(src => src.NomineeNames))
                .ForMember(dest => dest.Gender,
                    op => op.MapFrom(src => src.NomineeGender))
                .ForMember(dest => dest.DateOfBirth,
                    op => op.MapFrom(src => src.NomineeDateOfBirth))
                .ForMember(dest => dest.NationalIdentification,
                    op => op.MapFrom(src => src.NomineeNationalIdentification))
                .ForMember(dest => dest.PhysicalAddress,
                    op => op.MapFrom(src => src.NomineePhysicalAddress))
                .ForMember(dest => dest.MobileNumber,
                    op => op.MapFrom(src => src.NomineeMobileNumber))
                .ForMember(dest => dest.EmailAddress,
                    op => op.MapFrom(src => src.NomineeEmailAddress))
                .ForMember(dest => dest.Occupation,
                    op => op.MapFrom(src => src.NomineeOccupation))
                .ForMember(dest => dest.DateOfTakeUp,
                    op => op.MapFrom(src => src.NomineeDateOfAppointment));

            // NomineeBeneficiaryRequestDto => ShareholderSubscriptionDto
            CreateMap<NomineeBeneficiaryRequestDto, ShareholderSubscriptionDto>()
                .ForMember(dest => dest.Title,
                    op => op.MapFrom(src => src.ShareClass))
                .ForMember(dest => dest.Amount,
                    op => op.MapFrom(src => src.AmountSubscribed));

            // NomineeBeneficiaryRequestDto => NewShareHoldingEntityRequestDto
            CreateMap<NomineeBeneficiaryRequestDto, NewShareHoldingEntityRequestDto>()
                .ForMember(dest => dest.CountryCode,
                    op => op.MapFrom(src => src.BeneficiaryEntityCountry))
                .ForMember(dest => dest.Name,
                    op => op.MapFrom(src => src.BeneficiaryEntityName))
                .ForMember(dest => dest.CompanyReference,
                    op => op.MapFrom(src => src.BeneficiaryEntityReference));

            // TableOfArticlesDto => NewArticleOfAssociationRequestDto
            CreateMap<TableOfArticlesDto, NewArticleOfAssociationRequestDto>()
                .ForMember(dest => dest.TableOfArticles,
                    op => op.MapFrom(src => src.TableOfArticles.TableOfArticles));

            // AmendedArticleDto => NewAmendedArticleRequestDto
            CreateMap<AmendedArticleDto, NewAmendedArticleRequestDto>()
                .ForMember(dest => dest.Value,
                    op => op.MapFrom(src => src.Article));

            //
            CreateMap<AmendedArticlesDto, NewAmendedArticlesRequestDto>();

            CreateMap<MemorandumObjectsRequestDto, NewMemorandumOfAssociationObjectsRequestDto>();

            CreateMap<SingleObjectiveRequestDto, NewMemorandumOfAssociationObjectRequestDto>()
                .ForMember(dest => dest.Value, op =>
                    op.MapFrom(src => src.Objective));
        }
    }
}