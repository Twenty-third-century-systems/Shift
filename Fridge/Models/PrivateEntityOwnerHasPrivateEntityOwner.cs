namespace Fridge.Models {
    public class PrivateEntityOwnerHasPrivateEntityOwner {
        public PrivateEntityOwnerHasPrivateEntityOwner()
        {
            
        }
        public PrivateEntityOwnerHasPrivateEntityOwner(Person beneficiary, Person nominee)
        {
            Beneficiary=beneficiary;
            Nominee = nominee;
        }

        public int BeneficiaryId { get; set; }
        public int NomineeId { get; set; }

        public Person Beneficiary { get; set; }
        public Person Nominee { get; set; }
    }
}