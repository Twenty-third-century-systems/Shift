using System;
using System.Collections.Generic;
using Fridge.Constants;

#nullable disable

namespace Fridge.Models {
    public partial class PrivateEntity {
        public PrivateEntity()
        {
            Directors = new HashSet<Director>();
            PersonHoldsSharesInPrivateEntities = new HashSet<PersonHoldsSharesInPrivateEntity>();
            PersonRepresentsPrivateEntity = new HashSet<PersonRepresentsPrivateEntity>();
            PrivateEntitySubscriptions = new HashSet<PrivateEntitySubscription>();
        }

        public PrivateEntity(EntityName name) : this()
        {
            Name = name;
        }

        public int PrivateEntityId { get; set; }
        public int ApplicationId { get; set; }
        public int NameId { get; set; }
        public int? LastApplicationId { get; set; }
        public string IndustrySector { get; set; }
        public string Reference { get; set; }
        public Office Office { get; set; }
        public int? SecretaryId { get; set; }
        
        public ArticlesOfAssociation ArticlesOfAssociation { get; set; }
        public Application CurrentApplication { get; set; }
        public Application LastApplication { get; set; }
        public EntityName Name { get; set; }
        public MemorandumOfAssociation MemorandumOfAssociation { get; set; }

        // Secretary
        public Secretary Secretary { get; set; }
        // Directors
        public ICollection<Director> Directors { get; set; }
        // members
        public ICollection<PersonHoldsSharesInPrivateEntity> PersonHoldsSharesInPrivateEntities { get; set; }
        // Representative nominees in other private entities
        public ICollection<PersonRepresentsPrivateEntity> PersonRepresentsPrivateEntity { get; set; }
        // Subscriptions in other entities
        public ICollection<PrivateEntitySubscription> PrivateEntitySubscriptions { get; set; }
    }

    public class Office {
        public Address Address { get; set; }
        public string MobileNumber { get; set; }
        public string TelephoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public DateTime EffectiveFrom { get; set; }
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