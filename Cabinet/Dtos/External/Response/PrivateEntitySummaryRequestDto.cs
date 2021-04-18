using System.Collections.Generic;

namespace Cabinet.Dtos.External.Response {
    public class PrivateEntitySummaryRequestDto {
        public PrivateEntitySummaryRequestDto()
        {
            Directors = new List<Principal>();
            ArticleOfAssociation = new List<string>();
            Subscribers = new List<Subscriber>();
        }

        public string Reference { get; set; }
        public string Name { get; set; }
        public string DateOfIncorporation { get; set; }


        public string PhysicalAddress { get; set; }
        public string PostalAddress { get; set; }
        public string EmailAddress { get; set; }

        public Principal Secretary { get; set; }

        public List<Principal> Directors { get; set; }

        public string MajorObject { get; set; }
        
        public string LiabilityClause { get; set; }
        
        public string ShareCapital { get; set; }
        public string TableOfArticles { get; set; }
        public List<string> ArticleOfAssociation { get; set; }

        public List<Subscriber> Subscribers { get; set; }

        public class Principal {
            public string ChristianNames { get; set; }
            public string DateOfAppointment { get; set; }
            public string Ids { get; set; }
            public string Nationality { get; set; }
            public string PhysicalAddress { get; set; }
        }

        public class Subscriber {
            public string FullNamesAndIds { get; set; }
            public string OrdinaryShares { get; set; }
            public string PreferenceShares { get; set; }
            public string TotalShares { get; set; }
        }
    }
}