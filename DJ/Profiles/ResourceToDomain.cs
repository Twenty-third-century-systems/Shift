using AutoMapper;
using Cabinet.Dtos.Internal.Request;
using Fridge.Models;
using Fridge.Models.Main;

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