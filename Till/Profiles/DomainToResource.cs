using AutoMapper;
using Cabinet.Dtos.External.Response;
using Fridge.Models.Payments;

namespace Till.Profiles {
    public class DomainToResource : Profile {
        public DomainToResource()
        {
            // Transaction => TransactionResponseDto
            CreateMap<Transaction, TransactionResponseDto>()
                .ForMember(dest=>dest.Date, op=>
                    op.MapFrom(src=>src.Date.ToString("D")));

            // PriceItem => PriceListItemRequestDto
            CreateMap<PriceItem, PriceListItemRequestDto>();
        }
    }
}