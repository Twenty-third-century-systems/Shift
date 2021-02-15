namespace Fridge.Models {
    public class PrivateEntityOwnerHasShareClause {
        public int PrivateEntityOwnerId { get; set; }
        public int ShareClauseId { get; set; }
        public int Amount { get; set; }

        public PrivateEntityOwner Subscriber { get; set; }
        public ShareClause ShareClauseClass { get; set; }
    }
}