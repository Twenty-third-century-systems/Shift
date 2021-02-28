using System;
using System.Collections.Generic;
using Fridge.Constants;

#nullable disable

namespace Fridge.Models {
    public partial class PrivateEntity {
        public PrivateEntity()
        {
            Members = new HashSet<PrivateEntityHasPrivateEntityOwner>();
        }

        public PrivateEntity(Application nameSearchApplication)
        {
            NameSearchApplicationApplication = nameSearchApplication;
        }

        public int PrivateEntityId { get; set; }
        public int ApplicationId { get; set; }
        public int NameSearchApplicationId { get; set; }
        public int? LastApplicationId { get; set; }
        public string Reference { get; set; }
        public Office Office { get; set; }
        public ArticlesOfAssociation ArticlesOfAssociation { get; set; }

        public Application CurrentApplication { get; set; }
        public Application LastApplication { get; set; }
        public Application NameSearchApplicationApplication { get; set; }
        public ICollection<PrivateEntityHasPrivateEntityOwner> Members { get; set; }
        public ICollection<PrivateEntityHasPrivateEntity> MemberEntities { get; set; }
        public ICollection<PrivateEntityHasPrivateEntity> OwnedEntities { get; set; }
        public MemorandumOfAssociation MemorandumOfAssociation { get; set; }
    }

    public class Office {
        public Address Address { get; set; }
        public string MobileNumber { get; set; }
        public string TelephoneNumber { get; set; }
        public string EmailAddress { get; set; }
    }

    public class Address {
        public string PhysicalAddress { get; set; }
        public string PostalAddress { get; set; }
        public string CityTown { get; set; }
    }

    public class ArticlesOfAssociation {
        public ArticlesOfAssociation()
        {
            AmendedArticles = new HashSet<AmendedArticle>();
        }
        
        public EArticlesOfAssociation? TableOfArticles { get; set; }
        public ICollection<AmendedArticle> AmendedArticles { get; set; }
    }

    public class AmendedArticle {
        public int AmendedArticleId { get; set; }
        public string Value { get; set; }
    }

}