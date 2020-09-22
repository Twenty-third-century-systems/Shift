using System.Collections.Generic;

namespace DanceFlow.Models {
    public class MemorandumOfAssociation {
        public LiabilityShareClauses LiabilityShareClauses { get; set; }
        public List<SingleObjective> Objectives { get; set; }
    }
}