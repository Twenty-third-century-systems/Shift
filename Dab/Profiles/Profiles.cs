using AutoMapper;
using Cabinet.Dtos.External.Request;

namespace Dab.Profiles {
    public class Profiles:Profile {
        public Profiles()
        {
            // NewNameSearchFormRequestDto => NewNameSearchRequestDto
            CreateMap<NewNameSearchFormRequestDto, NewNameSearchRequestDto>();


            // PrivateOfficeAddressRequestDto => NewPrivateEntityAddressRequestDto
            CreateMap<PrivateOfficeAddressRequestDto, NewPrivateEntityAddressRequestDto>();
            
            
            // PrivateOfficeAddressRequestDto => NewPrivateEntityOfficeRequestDto
            CreateMap<PrivateOfficeAddressRequestDto, NewPrivateEntityOfficeRequestDto>();

        }
    }
}