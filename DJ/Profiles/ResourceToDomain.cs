using AutoMapper;
using Cabinet.Dtos.Internal.Request;
using Fridge.Models;

namespace DJ.Profiles {
    public class ResourceToDomain : Profile {
        public ResourceToDomain()
        {
            // NewTaskAllocationRequestDto => ExaminationTask
            CreateMap<NewTaskAllocationRequestDto, ExaminationTask>();
            
            // RaisedQueryRequestDto => RaisedQuery
            CreateMap<RaisedQueryRequestDto, RaisedQuery>();
        }
    }
}