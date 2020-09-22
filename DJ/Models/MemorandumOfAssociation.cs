using System.Collections.Generic;

namespace DJ.Models {
    public class MemorandumOfAssociation {
        public LiabilityShareClauses LiabilityShareClauses { get; set; }
        public List<SingleObjective> Objectives { get;} = new List<SingleObjective>();
    }
}