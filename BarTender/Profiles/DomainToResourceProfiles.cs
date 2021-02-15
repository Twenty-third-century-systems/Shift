using AutoMapper;
using Cabinet.Dtos.Response;
using Fridge.Models;

namespace BarTender.Profiles {
    public class DomainToResourceProfiles : Profile {
        public DomainToResourceProfiles()
        {
            // Application => SubmittedApplicationRequestDto
            CreateMap<Application, SubmittedApplicationResponseDto>()
                .ForMember(dest=>dest.Status,op=>
                    op.MapFrom(src=>src.Status.ToString()));
            
            
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
            
        }
    }
}