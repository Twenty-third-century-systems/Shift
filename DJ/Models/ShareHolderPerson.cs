namespace DJ.Models {
    public class ShareHolderPerson {        
        public string PeopleCountry { get; set; }
        public string NationalId { get; set; }
        public string MemberSurname { get; set; }
        public string MemberName { get; set; }
        public string Gender { get; set; }
        public string PhyAddress { get; set; }
        public bool IsSecretary { get; set; }
        public bool IsMember { get; set; }
        public bool IsDirector { get; set; }
        public long? OrdShares { get; set; }
        public long? PrefShares { get; set; }
    }
}