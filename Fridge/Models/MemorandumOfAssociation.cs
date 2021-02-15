using System.Collections.Generic;

namespace Fridge.Models {
    public class MemorandumOfAssociation {
        
        public MemorandumOfAssociation()
        {
            MemorandumObjects = new HashSet<MemorandumOfAssociationObject>();
            ShareClauses = new HashSet<ShareClause>();
        }

        public int MemorandumOfAssociationId { get; set; }
        public int PrivateEntityId { get; set; }
        public string LiabilityClause { get; set; }

        public PrivateEntity PrivateEntity { get; set; }
        public ICollection<MemorandumOfAssociationObject> MemorandumObjects { get; set; }
        public ICollection<ShareClause> ShareClauses { get; set; }
    }
}