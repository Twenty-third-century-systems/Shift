using AutoMapper;
using BarTender.DataModels;
using BarTender.Models;

namespace BarTender.Profiles {
    public class NameSearchProfile : Profile {
        public NameSearchProfile()
        {
            CreateMap<ReasonForSearch, Val>()
                .ForMember(
                    d => d.id,
                    opt => opt.MapFrom(
                        s => s.Id
                        )
                    )
                .ForMember(d => d.value,
                    opt => opt.MapFrom(
                        s => s.Description.ToUpper()
                        )
                    );
        }
    }
}