namespace Fridge.Models {
    public class PrivateEntityHasPrivateEntityOwner {
        public int PrivateEntityId { get; set; }
        public int PrivateEntityOwnerId { get; set; }

        public PrivateEntity Entity { get; set; }
        public PrivateEntityOwner Member { get; set; }
    }
}