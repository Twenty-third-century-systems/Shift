using AutoMapper;
using Cabinet.Dtos;
using Cabinet.Dtos.Request;
using Fridge.Models;

namespace BarTender.Profiles {
    public class ResourceToDomainProfile:Profile {
        public ResourceToDomainProfile()
        {
            // NewNameSearchRequestDto to NameSearch
            CreateMap<NewNameSearchRequestDto, NameSearch>();
            
            //SuggestedEntityNameRequestDto to EntityName
            CreateMap<SuggestedEntityNameRequestDto, EntityName>();
        }
    }
}