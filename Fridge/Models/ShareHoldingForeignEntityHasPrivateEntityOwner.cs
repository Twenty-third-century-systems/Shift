namespace Fridge.Models {
    public class ShareHoldingForeignEntityHasPrivateEntityOwner {
        public ShareHoldingForeignEntityHasPrivateEntityOwner()
        {
            
        }
        public ShareHoldingForeignEntityHasPrivateEntityOwner(ShareholdingForeignEntity shareholdingForeignEntity, Person mapPerson)
        {
            ForeignEntity = shareholdingForeignEntity;
            Nominee = mapPerson;
        }

        public int ForeignEntityId { get; set; }
        public int PrivateEntityOwnerId { get; set; }

        public ShareholdingForeignEntity ForeignEntity { get; set; }
        public Person Nominee { get; set; }        
    }
}