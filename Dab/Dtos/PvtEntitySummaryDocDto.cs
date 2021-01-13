using System;
using System.Collections.Generic;

namespace Dab.Dtos {
    public class PvtEntitySummaryDocDto {
        public class Company {
            public string Ref { get; set; }
            public string Name { get; set; }
            public string DateOfIncorporation { get; set; }
        }

        public class Address {
            public string SituatedAt { get; set; }
            public string PostalAddress { get; set; }
            public string EmailAddress { get; set; }
        }

        public class Principal {
            public string DateOfIncorporation { get; set; }
            public string ChristianNames { get; set; }
            public string Ids { get; set; }
            public string Nationality { get; set; }
            public string ResidentialAddress { get; set; }
        }

        public class Subscriber {
            public string FullNamesAndIds { get; set; }
            public string OrdinaryShares { get; set; }
            public string PreferenceShares { get; set; }
            public string TotalShares { get; set; }
        }

        public Company EntitySummary { get; set; }
        public Address EntityAddress { get; set; }

        public List<Principal> Directors = new List<Principal>();
        public List<Principal> Secretary = new List<Principal>();

        public string MajorObject { get; set; }
        public string LiabilityClause { get; set; }
        public string ShareCapital { get; set; }
        public List<string> ArticlesOfAssociation = new List<string>();

        public List<Subscriber> Subscribers = new List<Subscriber>();
    }
}