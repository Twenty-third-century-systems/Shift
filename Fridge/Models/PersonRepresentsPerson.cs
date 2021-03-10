namespace Fridge.Models {
    public class PersonRepresentsPerson {
        public PersonRepresentsPerson()
        {
            
        }
        public PersonRepresentsPerson(ShareHolder nominee, ShareHolder beneficiary)
        {
            Nominee = nominee;
            Beneficiary = beneficiary;
        }

        public int BeneficiaryId { get; set; }
        public int NomineeId { get; set; }

        public ShareHolder Beneficiary { get; set; }
        public ShareHolder Nominee { get; set; }
    }
}