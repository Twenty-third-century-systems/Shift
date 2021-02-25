namespace Fridge.Models {
    public class PrivateEntityOwnerHasPrivateEntityOwner {
        public PrivateEntityOwnerHasPrivateEntityOwner()
        {
            
        }
        public PrivateEntityOwnerHasPrivateEntityOwner(PrivateEntityOwner beneficiary, PrivateEntityOwner nominee)
        {
            Beneficiary=beneficiary;
            Nominee = nominee;
        }

        public int BeneficiaryId { get; set; }
        public int NomineeId { get; set; }

        public PrivateEntityOwner Beneficiary { get; set; }
        public PrivateEntityOwner Nominee { get; set; }
    }
}