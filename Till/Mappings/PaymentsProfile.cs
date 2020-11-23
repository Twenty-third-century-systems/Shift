using AutoMapper;
using Till.Dtos;
using Till.Models;

namespace Till.Mappings {
    public class PaymentsProfile : Profile {
        public PaymentsProfile()
        {
            CreateMap<TopupData, Payment>();
            
            CreateMap<PaymentDataDto, Payment>()
                .ForMember(dest => dest.DebitAmount,
                    options =>
                        options.MapFrom(src => src.Amount));  
        }
    }
}