namespace Fridge.Models {
    public class ShareHoldingForeignEntityHasPrivateEntityOwner {
        public int ForeignEntityId { get; set; }
        public int PrivateEntityOwnerId { get; set; }

        public ShareholdingForeignEntity ForeignEntity { get; set; }
        public PrivateEntityOwner Nominee { get; set; }        
    }
}