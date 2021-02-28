namespace Fridge.Models {
    public class PrivateEntityHasPrivateEntityOwner {
        public PrivateEntityHasPrivateEntityOwner()
        {
            
        }
        public PrivateEntityHasPrivateEntityOwner(PrivateEntity privateEntity, Person shareHolder)
        {
            Entity = privateEntity;
            Member = shareHolder;
        }

        public int PrivateEntityId { get; set; }
        public int PrivateEntityOwnerId { get; set; }

        public PrivateEntity Entity { get; set; }
        public Person Member { get; set; }
    }
}