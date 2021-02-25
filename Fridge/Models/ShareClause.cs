using System.Collections.Generic;

namespace Fridge.Models {
    public class ShareClause {
        public ShareClause()
        {
            Subscribers = new HashSet<PrivateEntityOwnerHasShareClause>();
            ForeignEntityShareHolders = new HashSet<ShareHoldingForeignEntityHasShareClause>();
        }
        public int ShareClauseId { get; set; }
        public string Title { get; set; }
        public double NominalValue { get; set; }
        public int MemorandumId { get; set; }

        public MemorandumOfAssociation MemorandumOfAssociation { get; set; }
        public ICollection<PrivateEntityOwnerHasShareClause> Subscribers { get; set; }
        public ICollection<ShareHoldingForeignEntityHasShareClause> ForeignEntityShareHolders { get; set; }
    }
}