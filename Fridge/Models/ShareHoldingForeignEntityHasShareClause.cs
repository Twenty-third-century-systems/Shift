namespace Fridge.Models {
    public class ShareHoldingForeignEntityHasShareClause {
        public ShareHoldingForeignEntityHasShareClause()
        {
        }

        public ShareHoldingForeignEntityHasShareClause(ShareholdingForeignEntity shareHolder, ShareClause shareClause,
            int amount)
        {
            Subscriber = shareHolder;
            ShareClauseClass = shareClause;
            Amount = amount;
        }

        public int ShareHoldingForeignEntityId { get; set; }
        public int ShareClauseId { get; set; }
        public int Amount { get; set; }

        public ShareholdingForeignEntity Subscriber { get; set; }
        public ShareClause ShareClauseClass { get; set; }
    }
}