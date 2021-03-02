namespace Fridge.Models {
    public class PersonRepresentsForeignEntity {
        public PersonRepresentsForeignEntity()
        {
        }

        public PersonRepresentsForeignEntity(ForeignEntity beneficiary, Person nominee)
        {
            Beneficiary = beneficiary;
            Nominee = nominee;
        }

        public int BeneficiaryId { get; set; }
        public int NomineeId { get; set; }

        public ForeignEntity Beneficiary { get; set; }
        public Person Nominee { get; set; }
    }
}