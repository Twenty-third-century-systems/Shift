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

        public int PrivateEntityId { get; set; }
        public int ApplicationId { get; set; }
        public int NameSearchApplicationId { get; set; }
        public int? LastApplicationId { get; set; }
        public string Reference { get; set; }
        public Office Office { get; set; }
        public ArticlesOfAssociation ArticlesOfAssociation { get; set; }

        public Application CurrentApplication { get; set; }
        public Application LastApplication { get; set; }
        public Application NameSearchApplication { get; set; }
        public ICollection<PrivateEntityHasPrivateEntityOwner> Members { get; set; }
        public MemorandumOfAssociation MemorandumOfAssociation { get; set; }

        public bool WasExaminedAndApproved()
        {
            return CurrentApplication.DateExamined != null && !string.IsNullOrEmpty(Reference);
        }
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
        public EArticlesOfAssociation? TableOfArticles { get; set; }
        public bool? Other { get; set; }
        public ICollection<AmendedArticle> AmendedArticles { get; set; }
    }

    public class AmendedArticle {
        public int AmendedArticleId { get; set; }
        public string Value { get; set; }
    }

}