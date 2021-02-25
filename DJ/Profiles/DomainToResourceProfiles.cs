using AutoMapper;
using Cabinet.Dtos.Internal.Response;
using Fridge.Models;

namespace DJ.Profiles {
    public class DomainToResourceProfiles : Profile {
        public DomainToResourceProfiles()
        {
            // Application => UnallocatedApplicationResponseDto
            CreateMap<Application, UnallocatedApplicationResponseDto>();
        }
    }
}