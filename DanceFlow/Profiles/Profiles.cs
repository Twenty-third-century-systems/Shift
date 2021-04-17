using AutoMapper;
using Cabinet.Dtos.Internal.Request;

namespace DanceFlow.Profiles {
    public class Profiles : Profile {
        public Profiles()
        {
            CreateMap<QueryRequestDto, RaisedQueryRequestDto>()
                .ForMember(dest => dest.Step, op => 
                    op.MapFrom(src => src.Query.Step))
                .ForMember(dest => dest.Comment, op => 
                    op.MapFrom(src => src.Query.Comment));
        }
    }
}