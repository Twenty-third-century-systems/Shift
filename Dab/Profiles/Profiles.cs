using AutoMapper;
using Cabinet.Dtos.External.Request;

namespace Dab.Profiles {
    public class Profiles : Profile {
        public Profiles()
        {
            // NewNameSearchFormRequestDto => NewNameSearchRequestDto
            CreateMap<NewNameSearchFormRequestDto, NewNameSearchRequestDto>();


            // PrivateOfficeAddressRequestDto => NewPrivateEntityAddressRequestDto
            CreateMap<PrivateOfficeAddressRequestDto, NewPrivateEntityAddressRequestDto>();


            // PrivateOfficeAddressRequestDto => NewPrivateEntityOfficeRequestDto
            CreateMap<PrivateOfficeAddressRequestDto, NewPrivateEntityOfficeRequestDto>();


            // 
            CreateMap<LiabilityClauseDto, NewLiabilityClauseRequestDto>()
                .ForMember(dest => dest.LiabilityClause,
                    op => op.MapFrom(src => src.Clause.LiabilityClause));
        }
    }
}