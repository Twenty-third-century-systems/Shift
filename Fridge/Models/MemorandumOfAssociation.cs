using System;
using System.Collections.Generic;

#nullable disable

namespace Fridge.Models
{
    public class MemorandumOfAssociation
    {
        public MemorandumOfAssociation()
        {
            MemorandumObjects = new HashSet<MemorandumObject>();
            PrivateEntities = new HashSet<PrivateEntity>();
        }

        public int MemorandumOfAssociationId { get; set; }
        public string ShareClause { get; set; }
        public string LiabilityClause { get; set; }

        public ICollection<MemorandumObject> MemorandumObjects { get; set; }
        public ICollection<PrivateEntity> PrivateEntities { get; set; }
    }
}
