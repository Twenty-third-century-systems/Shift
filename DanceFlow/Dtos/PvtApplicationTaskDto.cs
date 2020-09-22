using System.Collections.Generic;
using DanceFlow.Models;

namespace DanceFlow.Dtos {
    public class PvtApplicationTaskDto {
        public Application Application { get; set; }
        public Name Name { get; set; }
        public Office RegisteredRegOffice { get; set; }
        public PvtApplicationArticlesOfAssociation ArticlesOfAssociation { get; set; }
        public MemorandumOfAssociation MemorandumOfAssociation { get; set; }
        public List<ShareHolderPerson> ShareHolders { get; set; }
    }
}