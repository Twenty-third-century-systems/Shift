using AutoMapper;
using Till.Dtos;
using Till.Models;

namespace Till.Mappings {
    public class PaymentsProfile:Profile {
        public PaymentsProfile()
        {
            CreateMap<PaymentData, Payment>();
        }
    }
}