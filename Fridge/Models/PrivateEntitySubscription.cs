namespace Fridge.Models {
    public class PrivateEntitySubscription {
        public int PrivateEntityId { get; set; }
        public int ShareClauseId { get; set; }
        public int? Amount { get; set; }

        public  ShareClause ShareClause { get; set; }
        public  PrivateEntity PrivateEntity { get; set; }
    }
}