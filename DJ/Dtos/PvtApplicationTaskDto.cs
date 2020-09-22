using System.Collections.Generic;
using DJ.Models;

namespace DanceFlow.Dtos {
    public class PvtApplicationTaskDto {
        public Application Application { get; set; }
        public Name Name { get; set; }
        public RegOffice RegOffice { get; set; }
        public PvtApplicationArticlesOfAssociation ArticlesOfAssociation { get; set; }
        public MemorandumOfAssociation MemorandumOfAssociation { get; set; }
        public List<ShareHolderPerson> ShareHolders { get; set; }
    }
}