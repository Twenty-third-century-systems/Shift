using AutoMapper;
using Cabinet.Dtos.External.Request;

namespace Dab.Profiles {
    public class Profiles:Profile {
        public Profiles()
        {
            CreateMap<NewNameSearchFormRequestDto, NewNameSearchRequestDto>();
        }
    }
}