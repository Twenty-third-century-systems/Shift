using System.Linq;
using AutoMapper;
using Cabinet.Dtos.External.Response;
using Fridge.Constants;
using Fridge.Models;
using Fridge.Models.Main;
using Fridge.Models.Payments;

namespace BarTender.Profiles {
    public class DomainToResourceProfiles : Profile {
        public DomainToResourceProfiles()
        {
            // Application => SubmittedApplicationRequestDto
            CreateMap<Application, SubmittedApplicationSummaryResponseDto>()
                .ForMember(dest => dest.Status, op =>
                    op.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.DateSubmitted, op =>
                    op.MapFrom(src => src.DateSubmitted.ToString("d")));


            // NameSearch => NameSearchResponseDto
            CreateMap<NameSearch, NameSearchResponseDto>();


            //PrivateEntity => PrivateEntityResponceDto
            CreateMap<PrivateEntity, PrivateEntityResponseDto>();


            // City => SelectionValueResponseDto
            CreateMap<City, SelectionValueResponseDto>()
                .ForMember(dest => dest.Id,
                    op =>
                        op.MapFrom(src => src.CityId))
                .ForMember(dest => dest.Value,
                    op =>
                        op.MapFrom(src => src.Name));

            //Transaction => TransactionResponseDto
            CreateMap<Transaction, TransactionResponseDto>();


            // EntityName => RegisteredNameResponseDto
            CreateMap<EntityName, RegisteredNameResponseDto>()
                .ForMember(dest => dest.Id, op =>
                    op.MapFrom(src => src.EntityNameId))
                .ForMember(dest => dest.NameSearchReference, op =>
                    op.MapFrom(src => src.NameSearch.Reference))
                .ForMember(dest => dest.Name, op =>
                    op.MapFrom(src => src.Value.ToUpper()))
                .ForMember(dest => dest.DateSubmitted, op =>
                    op.MapFrom(src => src.NameSearch.Application.DateSubmitted.ToString("d")))
                .ForMember(dest => dest.ExpiryDate, op =>
                    op.MapFrom(src => src.NameSearch.ExpiryDate.Value.ToString("d")));

            // Application => ReservedNameRequestDto
            CreateMap<Application, ReservedNameRequestDto>()
                .ForMember(dest => dest.Reference, op =>
                    op.MapFrom(src => src.NameSearch.Reference))
                .ForMember(dest => dest.Value, op =>
                    op.MapFrom(src =>
                        src.NameSearch.Names
                            .SingleOrDefault(n => n.Status == ENameStatus.Reserved || n.Status == ENameStatus.Used)
                            .Value))
                .ForMember(dest => dest.ExpiryDate, op =>
                    op.MapFrom(src => src.NameSearch.ExpiryDate.Value.ToString("D")));

            // PrivateEntity => PrivateEntitySummaryRequestDto
            CreateMap<PrivateEntity, PrivateEntitySummaryRequestDto>()
                .ForMember(dest => dest.Name, op =>
                    op.MapFrom(src => src.Name.Value))
                .ForMember(dest => dest.DateOfIncorporation, op =>
                    op.MapFrom(src => src.CurrentApplication.DateExamined.Value.ToString("D")))
                .ForMember(dest => dest.PhysicalAddress, op =>
                    op.MapFrom(src => src.Office.Address.PhysicalAddress))
                .ForMember(dest => dest.PostalAddress, op =>
                    op.MapFrom(src => src.Office.Address.PostalAddress))
                .ForMember(dest => dest.EmailAddress, op =>
                    op.MapFrom(src => src.Office.EmailAddress))
                .ForMember(dest => dest.MajorObject, op =>
                    op.MapFrom(src => src.MemorandumOfAssociation.MemorandumObjects.First().Value))
                .ForMember(dest => dest.LiabilityClause, op =>
                    op.MapFrom(src => src.MemorandumOfAssociation.LiabilityClause))
                .ForMember(dest => dest.TableOfArticles, op =>
                    op.MapFrom(src => src.ArticlesOfAssociation.TableOfArticles));

            // Director => PrivateEntitySummaryRequestDto.Principal
            CreateMap<Director, PrivateEntitySummaryRequestDto.Principal>()
                .ForMember(dest => dest.ChristianNames, op =>
                    op.MapFrom(src => $"{src.Surname} {src.Names}"))
                .ForMember(dest => dest.DateOfAppointment, op =>
                    op.MapFrom(src => src.DateOfAppointment.ToString("D")))
                .ForMember(dest => dest.Ids, op =>
                    op.MapFrom(src => src.NationalIdentification))
                .ForMember(dest => dest.Nationality, op =>
                    op.MapFrom(src => src.Country.Name));
            
            // Secretary => PrivateEntitySummaryRequestDto.Principal
            CreateMap<Secretary, PrivateEntitySummaryRequestDto.Principal>()
                .ForMember(dest => dest.ChristianNames, op =>
                    op.MapFrom(src => $"{src.Surname} {src.Names}"))
                .ForMember(dest => dest.DateOfAppointment, op =>
                    op.MapFrom(src => src.DateOfAppointment.ToString("D")))
                .ForMember(dest => dest.Ids, op =>
                    op.MapFrom(src => src.NationalIdentification))
                .ForMember(dest => dest.Nationality, op =>
                    op.MapFrom(src => src.Country.Name));

            // ShareHolder => PrivateEntitySummaryRequestDto.Subscriber
            CreateMap<ShareHolder, PrivateEntitySummaryRequestDto.Subscriber>()
                .ForMember(dest => dest.FullNamesAndIds, op =>
                    op.MapFrom(src => $"{src.Surname} {src.Names} {src.NationalIdentification}"));

            // PrivateEntity => RegisteredPrivateEntityRequestDto
            CreateMap<PrivateEntity, RegisteredPrivateEntityRequestDto>()
                .ForMember(dest => dest.Name, op =>
                    op.MapFrom(src => src.Name.Value))
                .ForMember(dest => dest.DateRegistered, op =>
                    op.MapFrom(src => src.CurrentApplication.DateExamined));
        }
    }
}