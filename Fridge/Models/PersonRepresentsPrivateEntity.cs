namespace Fridge.Models {
    public class PersonRepresentsPrivateEntity {
        public PersonRepresentsPrivateEntity()
        {
            
        }
        public PersonRepresentsPrivateEntity(PrivateEntity beneficiary, ShareHolder nominee)
        {
            Beneficiary = beneficiary;
            Nominee = nominee;
        }

        public int NomineeId { get; set; }
        public int BeneficiaryId { get; set; }

        public  PrivateEntity Beneficiary { get; set; }
        public  ShareHolder Nominee { get; set; }
    }
}