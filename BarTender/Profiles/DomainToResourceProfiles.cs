using AutoMapper;
using Cabinet.Dtos;
using Cabinet.Dtos.Response;
using Fridge.Models;

namespace BarTender.Profiles {
    public class DomainToResourceProfiles : Profile {
        public DomainToResourceProfiles()
        {
            // ServiceApplication to SubmittedApplicationRequestDto
            CreateMap<ServiceApplication, SubmittedApplicationRequestDto>()
                .ForMember(dest => dest,
                    op =>
                        op.MapFrom(src => src.ServiceApplicationId))
                .ForMember(dest => dest.Service,
                    op =>
                        op.MapFrom(src => src.ServiceType.Description))
                .ForMember(dest => dest.DateSubmitted,
                    op =>
                        op.MapFrom(src => src.FormattedDateOfSubmission()))
                .ForMember(dest => dest.Status,
                    op =>
                        op.MapFrom(src => src.ApplicationStatus.Description));

            // NameSearch to SubmittedApplicationRequestDto
            CreateMap<NameSearch, SubmittedApplicationRequestDto>()
                .ForMember(dest => dest.Id,
                    op =>
                        op.MapFrom(src => src.ServiceApplication.ServiceApplicationId))
                .ForMember(dest => dest.Service,
                    op =>
                        op.MapFrom(src => src.ServiceApplication.ServiceType.Description))
                .ForMember(dest => dest.DateSubmitted,
                    op =>
                        op.MapFrom(src => src.ServiceApplication.FormattedDateOfSubmission()))
                .ForMember(dest => dest.Status,
                    op =>
                        op.MapFrom(src => src.ServiceApplication.ApplicationStatus.Description));

            // PrivateEntity to SubmittedApplicationRequestDto
            CreateMap<PrivateEntity, SubmittedApplicationRequestDto>()
                .ForMember(dest => dest.Id,
                    op =>
                        op.MapFrom(src => src.ServiceApplication.ServiceApplicationId))
                .ForMember(dest => dest.Service,
                    op =>
                        op.MapFrom(src => src.ServiceApplication.ServiceType.Description))
                .ForMember(dest => dest.DateSubmitted,
                    op =>
                        op.MapFrom(src => src.ServiceApplication.FormattedDateOfSubmission()))
                .ForMember(dest => dest.Status,
                    op =>
                        op.MapFrom(src => src.ServiceApplication.ApplicationStatus.Description))
                .ForMember(dest => dest,
                    op =>
                        op.MapFrom(src => src.EntityName.NameSearchId));
            
            // ReasonForSearch => SelectionValueResponseDto
            CreateMap<ReasonForNameSearch, SelectionValueResponseDto>()
                .ForMember(dest => dest.Id,
                    op =>
                        op.MapFrom(src => src.ReasonForNameSearchId))
                .ForMember(dest => dest.Value,
                    op =>
                        op.MapFrom(src => src.Description));
            
            // ServiceType => SelectionValueResponseDto
            CreateMap<ServiceType, SelectionValueResponseDto>()
                .ForMember(dest => dest.Id,
                    op =>
                        op.MapFrom(src => src.ServiceTypeId))
                .ForMember(dest => dest.Value,
                    op =>
                        op.MapFrom(src => src.Description));
            
            // Designation => SelectionValueResponseDto
            CreateMap<Designation, SelectionValueResponseDto>()
                .ForMember(dest => dest.Id,
                    op =>
                        op.MapFrom(src => src.DesignationId))
                .ForMember(dest => dest.Value,
                    op =>
                        op.MapFrom(src => src.Description));
            
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