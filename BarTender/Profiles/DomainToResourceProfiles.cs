using AutoMapper;
using Cabinet.Dtos.External.Response;
using Fridge.Models;

namespace BarTender.Profiles {
    public class DomainToResourceProfiles : Profile {
        public DomainToResourceProfiles()
        {
            // Application => SubmittedApplicationRequestDto
            CreateMap<Application, SubmittedApplicationSummaryResponseDto>()
                .ForMember(dest => dest.Status, op =>
                    op.MapFrom(src => src.Status.ToString()))
                .ForMember(dest=>dest.DateSubmitted, op=>
                    op.MapFrom(src=>src.DateSubmitted.ToString("d")));


            // NameSearch => NameSearchResponseDto
            CreateMap<NameSearch, NameSearchResponseDto>();


            //PrivateEntity => PrivateEntityResponceDto
            CreateMap<PrivateEntity, PrivateEntityResponseDto>();


            // City => SelectionValueResponseDto
            CreateMap<City, SelectionValueResponseDto>()
                .ForMember(dest => dest.Id,
                    op =>
                        op.MapFrom(src => src.CityId))
                .ForMember(dest => dest.Value,
                    op =>
                        op.MapFrom(src => src.Name));

            //Transaction => TransactionResponseDto
            CreateMap<Transaction, TransactionResponseDto>();


            // EntityName => RegisteredNameResponseDto
            CreateMap<EntityName, RegisteredNameResponseDto>()
                .ForMember(dest => dest.Id, op =>
                    op.MapFrom(src => src.EntityNameId))
                .ForMember(dest => dest.NameSearchReference, op =>
                    op.MapFrom(src => src.NameSearch.Reference))
                .ForMember(dest => dest.Name, op =>
                    op.MapFrom(src => src.Value.ToUpper()))
                .ForMember(dest => dest.DateSubmitted, op =>
                    op.MapFrom(src => src.NameSearch.Application.DateSubmitted.ToString("d")))
                .ForMember(dest => dest.ExpiryDate, op =>
                    op.MapFrom(src => src.NameSearch.ExpiryDate.Value.ToString("d")));
        }
    }
}