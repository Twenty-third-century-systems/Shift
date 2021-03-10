namespace Fridge.Models {
    public class PersonSubscription {
        public PersonSubscription()
        {
            
        }
        public PersonSubscription(ShareHolder shareHolder, ShareClause shareClause, int amountOfSharesSubscribed)
        {
            ShareHolder = shareHolder;
            ShareClause = shareClause;
            AmountOfSharesSubscribed = amountOfSharesSubscribed;
        }

        public int PersonId { get; set; }
        public int ShareClauseId { get; set; }
        public int AmountOfSharesSubscribed { get; set; }

        public ShareClause ShareClause { get; set; }
        public ShareHolder ShareHolder { get; set; }
    }
}