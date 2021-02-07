using System;
using System.Collections.Generic;

#nullable disable

namespace Fridge.Models {
    public partial class PrivateEntity {
        public PrivateEntity()
        {
            PvtEntityHasForeignEntities = new HashSet<PrivateEntityHasForeignEntity>();
            PvtEntityHasPvtEntityOwnedNavigations = new HashSet<PrivateEntityHasPrivateEntity>();
            PvtEntityHasPvtEntityOwnerNavigations = new HashSet<PrivateEntityHasPrivateEntity>();
            PvtEntityHasSubcribers = new HashSet<PrivateEntityHasSubscriber>();
        }

        public int PrivateEntityId { get; set; }
        public int ApplicationId { get; set; }
        public int? LastApplicationId { get; set; }
        public int? EntityNameId { get; set; }
        public int? EntityOfficeId { get; set; }
        public int? ArticlesOfAssociationId { get; set; }
        public int? MemorandumOfAssociationId { get; set; }
        public string Reference { get; set; }

        public ServiceApplication ServiceApplication { get; set; }
        public ServiceApplication LastApplication { get; set; }
        public ArticleOfAssociation ArticleOfAssociation { get; set; }
        public MemorandumOfAssociation MemorandumOfAssociation { get; set; }
        public EntityName EntityName { get; set; }
        public PrivateEntityOffice PrivateEntityOffice { get; set; }
        public ICollection<PrivateEntityHasForeignEntity> PvtEntityHasForeignEntities { get; set; }
        public ICollection<PrivateEntityHasPrivateEntity> PvtEntityHasPvtEntityOwnedNavigations { get; set; }
        public ICollection<PrivateEntityHasPrivateEntity> PvtEntityHasPvtEntityOwnerNavigations { get; set; }
        public ICollection<PrivateEntityHasSubscriber> PvtEntityHasSubcribers { get; set; }
        public ICollection<PrivateEntitySubscriber> PrivateEntitySubscribers { get; set; }

        public bool WasExaminedAndApproved()
        {
            return ServiceApplication.DateExamined != null && !string.IsNullOrEmpty(Reference);
        }

        class Office {
            public int PrivateEntityOfficeId { get; set; }
            public string PhysicalAddress { get; set; }
            public string PostalAddress { get; set; }
            public int CityId { get; set; }
            public string MobileNumber { get; set; }
            public string TelephoneNumber { get; set; }
            public string EmailAddress { get; set; }
        }

        class ArticlesOfAssociation {
            public int ArticleOfAssociationId { get; set; }
            public bool TableA { get; set; }
            public bool TableB { get; set; }
            public bool Other { get; set; }
        }

        class AmendedArticle {            
            public int AmendedArticleId { get; set; }
            public string Value { get; set; }
            public int ArticleId { get; set; }
        }

        class Memorandum {
            public int MemorandumOfAssociationId { get; set; }
            public string ShareClause { get; set; }
            public string LiabilityClause { get; set; }
        }

        class MemoObject {
            public int MemorandumObjectId { get; set; }
            public string Value { get; set; }
            public int MemorandumId { get; set; }
        }
    }
}