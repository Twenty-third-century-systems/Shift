namespace Fridge.Models {
    public class PrivateEntityOwnerHasPrivateEntityOwner {
        public int BeneficiaryId { get; set; }
        public int NomineeId { get; set; }

        public PrivateEntityOwner Beneficiary { get; set; }
        public PrivateEntityOwner Nominee { get; set; }
    }
}