﻿using AutoMapper;
using Cabinet.Dtos.External.Request;
using Fridge.Models;

namespace BarTender.Profiles {
    public class ResourceToDomainProfile : Profile {
        public ResourceToDomainProfile()
        {
            // NewNameSearchRequestDto => NameSearch
            CreateMap<NewNameSearchRequestDto, NameSearch>();

            //SuggestedEntityNameRequestDto => EntityName
            CreateMap<SuggestedEntityNameRequestDto, EntityName>();
            
            // NewPrivateEntityAddressRequestDto => Address
            CreateMap<NewPrivateEntityAddressRequestDto, Address>();
            
            // NewPrivateEntityOfficeRequestDto => Office
            CreateMap<NewPrivateEntityOfficeRequestDto, Office>();
            
            // NewMemorandumRequestDto => MemorandumOfAssociation
            CreateMap<NewMemorandumRequestDto, MemorandumOfAssociation>();
            
            // NewShareClauseRequestDto => ShareClause
            CreateMap<NewShareClauseRequestDto, ShareClause>();
            
            // NewMemorandumOfAssociationObjectRequestDto => MemorandumOfAssociationObject
            CreateMap<NewMemorandumOfAssociationObjectRequestDto, MemorandumOfAssociationObject>();
            
            // NewAmendedArticleRequestDto => AmendedArticle
            CreateMap<NewAmendedArticleRequestDto, AmendedArticle>();
            
            // NewArticleOfAssociationRequestDto => ArticlesOfAssociation
            CreateMap<NewArticleOfAssociationRequestDto, ArticlesOfAssociation>();
            
            // NewShareHolderRequestDto => PrivateEntityOwner
            CreateMap<NewShareHolderRequestDto, PrivateEntityOwner>();
        }
    }
}