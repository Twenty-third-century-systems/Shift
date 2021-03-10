namespace Fridge.Models {
    public class PersonRepresentsForeignEntity {
        public PersonRepresentsForeignEntity()
        {
        }

        public PersonRepresentsForeignEntity(ForeignEntity beneficiary, ShareHolder nominee)
        {
            Beneficiary = beneficiary;
            Nominee = nominee;
        }

        public int BeneficiaryId { get; set; }
        public int NomineeId { get; set; }

        public ForeignEntity Beneficiary { get; set; }
        public ShareHolder Nominee { get; set; }
    }
}