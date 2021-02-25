namespace Fridge.Models {
    public class ShareHoldingForeignEntityHasPrivateEntityOwner {
        public ShareHoldingForeignEntityHasPrivateEntityOwner()
        {
            
        }
        public ShareHoldingForeignEntityHasPrivateEntityOwner(ShareholdingForeignEntity shareholdingForeignEntity, PrivateEntityOwner mapPrivateEntityOwner)
        {
            ForeignEntity = shareholdingForeignEntity;
            Nominee = mapPrivateEntityOwner;
        }

        public int ForeignEntityId { get; set; }
        public int PrivateEntityOwnerId { get; set; }

        public ShareholdingForeignEntity ForeignEntity { get; set; }
        public PrivateEntityOwner Nominee { get; set; }        
    }
}