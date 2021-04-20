namespace Fridge.Models.Main {
    public class ForeignEntitySubscription {
        public ForeignEntitySubscription()
        {
        }

        public ForeignEntitySubscription(ForeignEntity foreignEntity, ShareClause shareClause,
            int amountOfSharesSubscribed)
        {
            ForeignEntity = foreignEntity;
            ShareClause = shareClause;
            AmountOfSharesSubscribed = amountOfSharesSubscribed;
        }

        public int ForeignEntityId { get; set; }
        public int ShareClauseId { get; set; }
        public int AmountOfSharesSubscribed { get; set; }

        public ShareClause ShareClause { get; set; }
        public ForeignEntity ForeignEntity { get; set; }
    }
}