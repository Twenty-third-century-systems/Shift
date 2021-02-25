namespace Fridge.Models {
    public class PrivateEntityOwnerHasShareClause {
        public PrivateEntityOwnerHasShareClause()
        {
            
        }
        public PrivateEntityOwnerHasShareClause(PrivateEntityOwner shareHolder, ShareClause shareClause, int amount)
        {
            Subscriber = shareHolder;
            ShareClauseClass = shareClause;
            Amount = amount;
        }

        public int PrivateEntityOwnerId { get; set; }
        public int ShareClauseId { get; set; }
        public int Amount { get; set; }

        public PrivateEntityOwner Subscriber { get; set; }
        public ShareClause ShareClauseClass { get; set; }
    }
}